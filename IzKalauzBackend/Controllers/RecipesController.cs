using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using IzKalauzBackend.Data;
using IzKalauzBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace IzKalauzBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RecipesController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET: /api/Recipes?page=1&pageSize=10
        // ÖSSZES RECEPT (LAPOZOTT)
        // =========================
        [HttpGet]
        public async Task<IActionResult> GetAllRecipes(int page = 1, int pageSize = 8)
        {
            try
            {
                var query = _context.Recipes
                    .Include(r => r.Ingredients)
                    .OrderByDescending(r => r.CreatedAt);
                var totalCount = await query.CountAsync();
                var recipes = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

                return Ok(new { items = recipes, totalCount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Hiba történt a receptek lekérésekor.", error = ex.Message });
            }
        }

        // ==================================
        // GET: /api/Recipes/my?page=1&pageSize=10
        // SAJÁT RECEPTEK (LAPOZOTT)
        // ==================================
        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetMyRecipes(int page = 1, int pageSize = 8)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
                return Unauthorized(new { message = "A token nem tartalmaz érvényes emailt." });

            try
            {
                var query = _context.Recipes
                    .Include(r => r.Ingredients)
                    .Where(r => r.AuthorEmail == email)
                    .OrderByDescending(r => r.CreatedAt);

                var totalCount = await query.CountAsync();
                var myRecipes = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

                return Ok(new { items = myRecipes, totalCount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Hiba történt a saját receptek lekérésekor.", error = ex.Message });
            }
        }

        // =========================
        // POST: /api/Recipes
        // =========================
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateRecipe([FromBody] Recipe request)
        {
            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.Title))
                    return BadRequest(new { message = "A recept címe kötelező." });

                if (string.IsNullOrWhiteSpace(request.Category))
                    return BadRequest(new { message = "A kategória kötelező." });

                if (string.IsNullOrWhiteSpace(request.Description))
                    return BadRequest(new { message = "A leírás kötelező." });

                if (string.IsNullOrWhiteSpace(request.HowToText))
                    return BadRequest(new { message = "Az elkészítés menete kötelező." });

                var email = User.FindFirstValue(ClaimTypes.Email);
                if (string.IsNullOrEmpty(email))
                    return Unauthorized(new { message = "A token nem tartalmaz érvényes emailt." });

                var recipe = new Recipe
                {
                    Id = Guid.NewGuid(),
                    Title = request.Title,
                    Category = request.Category,
                    Description = request.Description,
                    HowToText = request.HowToText,
                    AuthorEmail = email,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Ingredients = new List<Ingredient>()
                };

                // Hozzávalók hozzáadása
                if (request.Ingredients != null && request.Ingredients.Any())
                {
                    foreach (var ing in request.Ingredients)
                    {
                        recipe.Ingredients.Add(new Ingredient
                        {
                            Id = Guid.NewGuid(),
                            Name = ing.Name,
                            Quantity = ing.Quantity,
                            Unit = ing.Unit,
                            RecipeId = recipe.Id
                        });
                    }
                }

                _context.Recipes.Add(recipe);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Recept sikeresen létrehozva.", recipe });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Hiba történt a recept létrehozásakor.",
                    error = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }

        // =========================
        // GET: /api/Recipes/{id}
        // =========================
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetRecipe(Guid id)
        {
            var recipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (recipe == null)
                return NotFound(new { message = "Recept nem található." });

            return Ok(recipe);
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateRecipe(Guid id, [FromBody] Recipe updated)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            var recipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null)
                return NotFound(new { message = "Recept nem található." });

            if (user == null || (user.Role != "Admin" && recipe.AuthorEmail != email))
                return Forbid();

            // Alapadatok frissítése
            recipe.Title = updated.Title;
            recipe.Category = updated.Category;
            recipe.Description = updated.Description;
            recipe.HowToText = updated.HowToText;
            recipe.UpdatedAt = DateTime.UtcNow;

            // Hozzávalók törlése az adatbázisból
            var existingIngredients = await _context.Ingredients
                .Where(i => i.RecipeId == id)
                .ToListAsync();

            _context.Ingredients.RemoveRange(existingIngredients);

            // Új hozzávalók hozzáadása
            if (updated.Ingredients != null && updated.Ingredients.Any())
            {
                foreach (var ing in updated.Ingredients)
                {
                    _context.Ingredients.Add(new Ingredient
                    {
                        Id = Guid.NewGuid(),
                        Name = ing.Name,
                        Quantity = ing.Quantity,
                        Unit = ing.Unit,
                        RecipeId = recipe.Id
                    });
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Recept sikeresen frissítve.", recipe });
        }

        // =========================
        // DELETE: /api/Recipes/{id}
        // =========================
        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteRecipe(Guid id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
                return NotFound(new { message = "Recept nem található." });

            // Admin minden receptet törölhet, normál user csak a sajátját
            if (user == null || (user.Role != "Admin" && recipe.AuthorEmail != email))
                return Forbid();

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Recept törölve." });
        }

        // =========================
        // POST: /api/Recipes/{id}/image
        // KÉP FELTÖLTÉS ADMINNAK
        // =========================
        [Authorize]
        [HttpPost("{id:guid}/image")]
        public async Task<IActionResult> UploadRecipeImage(Guid id, IFormFile file)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
                return NotFound("Recept nem található.");

            if (file == null || file.Length == 0)
                return BadRequest("Nincs kiválasztva fájl.");

            var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "recipes");
            if (!Directory.Exists(uploadDir))
                Directory.CreateDirectory(uploadDir);

            var fileExt = Path.GetExtension(file.FileName);
            var fileName = $"{id}_{DateTime.Now.Ticks}{fileExt}";
            var filePath = Path.Combine(uploadDir, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            recipe.ImagePath = Path.Combine("images", "recipes", fileName).Replace("\\", "/");
            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();

            return Ok(new { imagePath = recipe.ImagePath });
        }

    }
}