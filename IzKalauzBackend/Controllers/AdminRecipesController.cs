using IzKalauzBackend.Data;
using IzKalauzBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IzKalauzBackend.Controllers
{
    [Route("api/admin/recipes")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminRecipesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminRecipesController(AppDbContext context)
        {
            _context = context;
        }

        // --- Admin listáz minden receptet ---
        [HttpGet]
        public async Task<IActionResult> GetAllRecipes()
        {
            var recipes = await _context.Recipes
                .Select(r => new
                {
                    r.Id,
                    r.Title,
                    r.Status,
                    r.AuthorEmail,
                    r.ImagePath
                })
                .ToListAsync();

            return Ok(recipes);
        }

        // --- Admin módosítja a státuszt ---
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateRecipeStatus(Guid id, [FromBody] string status)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
                return NotFound("Recept nem található");

            // Ellenőrizzük a státusz érvényességét
            if (status != "Pending" && status != "Approved" && status != "Rejected")
                return BadRequest("Érvénytelen státusz: Pending, Approved, vagy Rejected kell legyen.");

            recipe.Status = status;
            recipe.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new { recipe.Id, recipe.Title, recipe.Status });
        }
    }
}
