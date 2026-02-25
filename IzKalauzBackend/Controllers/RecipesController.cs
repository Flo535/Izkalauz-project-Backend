using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using IzKalauzBackend.Data;
using IzKalauzBackend.Models;

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

        // GET: api/Recipes
        [HttpGet]
        public async Task<IActionResult> GetRecipes(int page = 1, int pageSize = 8)
        {
            var query = _context.Recipes.AsNoTracking();

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderByDescending(r => r.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new { items, totalCount });
        }

        // GET: api/Recipes/my
        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetMyRecipes()
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(email)) return Unauthorized();

            var items = await _context.Recipes
                .Where(r => r.AuthorEmail == email)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return Ok(new { items, totalCount = items.Count });
        }

        // GET: api/Recipes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipe(Guid id)
        {
            var recipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null) return NotFound();
            return Ok(recipe);
        }

        // PUT: api/Recipes/{id} - SZERKESZTÉS (Admin hatalommal)
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutRecipe(Guid id, [FromBody] Recipe updatedRecipe)
        {
            var recipe = await _context.Recipes.Include(r => r.Ingredients).FirstOrDefaultAsync(r => r.Id == id);
            if (recipe == null) return NotFound();

            var userEmail = User.FindFirstValue(ClaimTypes.Email) ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");

            // Ha nem a szerző és nem admin, tiltjuk
            if (recipe.AuthorEmail != userEmail && !isAdmin) return Forbid();

            // Mezők frissítése a te Recipe.cs fájlod alapján
            recipe.Title = updatedRecipe.Title;
            recipe.Category = updatedRecipe.Category;
            recipe.Description = updatedRecipe.Description;
            recipe.HowToText = updatedRecipe.HowToText;
            recipe.IsApproved = updatedRecipe.IsApproved;
            recipe.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Recipes/{id}/image - KÉPFELTÖLTÉS (Admin hatalommal)
        [HttpPost("{id}/image")]
        [Authorize]
        public async Task<IActionResult> UploadImage(Guid id, IFormFile file)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return NotFound("A recept nem található.");

            var userEmail = User.FindFirstValue(ClaimTypes.Email) ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");

            if (recipe.AuthorEmail != userEmail && !isAdmin) return Forbid();
            if (file == null || file.Length == 0) return BadRequest("Érvénytelen fájl.");

            string folderName = Path.Combine("images", "recipes");
            string fullPath = Path.Combine(_env.WebRootPath, folderName);

            if (!Directory.Exists(fullPath)) Directory.CreateDirectory(fullPath);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(fullPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            if (!string.IsNullOrEmpty(recipe.ImagePath) && !recipe.ImagePath.Contains("default.jpg"))
            {
                string oldFileName = Path.GetFileName(recipe.ImagePath);
                string oldFilePath = Path.Combine(fullPath, oldFileName);
                if (System.IO.File.Exists(oldFilePath))
                {
                    try { System.IO.File.Delete(oldFilePath); } catch { }
                }
            }

            recipe.ImagePath = fileName;
            await _context.SaveChangesAsync();

            return Ok(new { imagePath = fileName });
        }

        // DELETE: api/Recipes/{id} - TÖRLÉS (Admin hatalommal)
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteRecipe(Guid id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return NotFound();

            var userEmail = User.FindFirstValue(ClaimTypes.Email) ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");

            if (recipe.AuthorEmail != userEmail && !isAdmin) return Forbid();

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}