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
                    // A bool IsApproved értékéből szöveges státuszt generálunk a Frontendnek
                    Status = r.IsApproved ? "Approved" : "Pending",
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

            // Ellenőrizzük a beérkező szöveget és beállítjuk az IsApproved értékét
            if (status == "Approved")
            {
                recipe.IsApproved = true;
            }
            else if (status == "Pending")
            {
                recipe.IsApproved = false;
            }
            else if (status == "Rejected")
            {
                // Ha elutasítjuk, nálunk az a törlést jelenti, vagy maradjon Pending?
                // Itt most csak simán false-ra állítjuk, de törölheted is a receptet:
                // _context.Recipes.Remove(recipe); 
                recipe.IsApproved = false;
            }
            else
            {
                return BadRequest("Érvénytelen státusz: Pending, Approved, vagy Rejected kell legyen.");
            }

            recipe.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            // Visszaadjuk az eredményt a Frontendnek a várt formátumban
            return Ok(new
            {
                recipe.Id,
                recipe.Title,
                Status = recipe.IsApproved ? "Approved" : "Pending"
            });
        }
    }
}