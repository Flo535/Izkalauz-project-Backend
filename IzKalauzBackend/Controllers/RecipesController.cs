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

        // GET: /api/Recipes
        [HttpGet]
        public async Task<IActionResult> GetAllRecipes()
        {
            try
            {
                var recipes = await _context.Recipes.ToListAsync();
                return Ok(recipes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Hiba történt a receptek lekérésekor.",
                    error = ex.Message
                });
            }
        }

        // GET: /api/Recipes/my
        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetMyRecipes()
        {
            var email = User.FindFirstValue(ClaimTypes.Email); // JWT-ben lévő email
            if (string.IsNullOrEmpty(email))
                return Unauthorized(new { message = "A token nem tartalmaz érvényes emailt." });

            try
            {
                var myRecipes = await _context.Recipes
                    .Where(r => r.AuthorEmail == email)
                    .ToListAsync();

                return Ok(myRecipes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Hiba történt a saját receptek lekérésekor.",
                    error = ex.Message
                });
            }
        }


        // POST: /api/Recipes (JWT szükséges)
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateRecipe([FromBody] Recipe request)
        {
            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.Title))
                    return BadRequest(new { message = "A recept címe kötelező." });

                var email = User.FindFirstValue(ClaimTypes.Email);
                if (string.IsNullOrEmpty(email))
                    return Unauthorized(new { message = "A token nem tartalmaz érvényes emailt." });

                var recipe = new Recipe
                {
                    Id = Guid.NewGuid(),
                    Title = request.Title,
                    Description = request.Description ?? "",
                    Ingredients = request.Ingredients ?? new List<string>(),
                    AuthorEmail = email,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

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

        // GET: /api/Recipes/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetRecipe(Guid id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
                return NotFound(new { message = "Recept nem található." });

            return Ok(recipe);
        }

        // PUT: /api/Recipes/{id}
        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateRecipe(Guid id, [FromBody] Recipe updated)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
                return NotFound(new { message = "Recept nem található." });

            if (recipe.AuthorEmail != email)
                return Forbid();

            recipe.Title = updated.Title;
            recipe.Description = updated.Description;
            recipe.Ingredients = updated.Ingredients ?? new List<string>();
            recipe.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Recept sikeresen frissítve.", recipe });
        }

        // DELETE: /api/Recipes/{id}
        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteRecipe(Guid id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
                return NotFound(new { message = "Recept nem található." });

            if (recipe.AuthorEmail != email)
                return Forbid();

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Recept törölve." });
        }
    }
}
