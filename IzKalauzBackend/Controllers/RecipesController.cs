using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using IzKalauzBackend.Data;
using IzKalauzBackend.Models;
using System.Text.Json;

namespace IzKalauzBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public RecipesController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecipes()
        {
            var recipes = await _context.Recipes
                .Include(r => r.Ingredients)
                .Where(r => r.IsApproved)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
            return Ok(new { items = recipes });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipe(Guid id)
        {
            var recipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (recipe == null) return NotFound();
            return Ok(recipe);
        }

        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetMyRecipes()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var items = await _context.Recipes
                .Include(r => r.Ingredients)
                .Where(r => r.AuthorEmail == email)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
            return Ok(new { items });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateRecipe([FromForm] Recipe recipe, [FromForm] string? ingredientsJson, IFormFile? image)
        {
            try
            {
                var email = User.FindFirstValue(ClaimTypes.Email) ?? "ismeretlen";
                recipe.Id = Guid.NewGuid();
                recipe.AuthorEmail = email;
                recipe.CreatedAt = DateTime.UtcNow;
                recipe.UpdatedAt = DateTime.UtcNow;
                recipe.IsApproved = User.IsInRole("Admin");

                if (!string.IsNullOrEmpty(ingredientsJson))
                {
                    recipe.Ingredients = JsonSerializer.Deserialize<List<Ingredient>>(ingredientsJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Ingredient>();
                    foreach (var ing in recipe.Ingredients) ing.Id = Guid.NewGuid();
                }

                if (image != null)
                {
                    string folder = Path.Combine(_env.WebRootPath, "images/recipes");
                    if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                    string fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                    using (var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create)) await image.CopyToAsync(stream);
                    recipe.ImagePath = $"/images/recipes/{fileName}";
                }

                _context.Recipes.Add(recipe);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Mentve!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a létrehozáskor: {ex.Message}");
                return StatusCode(500, $"Hiba: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutRecipe(Guid id, [FromForm] Recipe updated, [FromForm] string? ingredientsJson, IFormFile? image)
        {
            try
            {
                var recipe = await _context.Recipes.Include(r => r.Ingredients).FirstOrDefaultAsync(r => r.Id == id);
                if (recipe == null) return NotFound();

                bool isAdmin = User.IsInRole("Admin");
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                if (!isAdmin && recipe.AuthorEmail != userEmail) return Forbid();

                recipe.Title = updated.Title;
                recipe.Category = updated.Category;
                recipe.Description = updated.Description;
                recipe.HowToText = updated.HowToText;
                recipe.UpdatedAt = DateTime.UtcNow;
                recipe.IsApproved = isAdmin;

                if (image != null)
                {
                    string folder = Path.Combine(_env.WebRootPath, "images/recipes");
                    if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                    string fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                    using (var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create)) await image.CopyToAsync(stream);
                    recipe.ImagePath = $"/images/recipes/{fileName}";
                }

                var oldIngredients = await _context.Ingredients.Where(i => i.RecipeId == id).ToListAsync();
                _context.Ingredients.RemoveRange(oldIngredients);
                await _context.SaveChangesAsync();

                if (!string.IsNullOrEmpty(ingredientsJson))
                {
                    var ingredients = JsonSerializer.Deserialize<List<Ingredient>>(ingredientsJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (ingredients != null)
                    {
                        foreach (var ing in ingredients)
                        {
                            _context.Ingredients.Add(new Ingredient
                            {
                                Id = Guid.NewGuid(),
                                RecipeId = recipe.Id,
                                Name = ing.Name,
                                Quantity = ing.Quantity,
                                Unit = ing.Unit
                            });
                        }
                    }
                }

                await _context.SaveChangesAsync();
                return Ok(new { message = "Sikeres mentés!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a szerkesztéskor: {ex.Message}");
                return StatusCode(500, "Mentési hiba.");
            }
        }
    }
}