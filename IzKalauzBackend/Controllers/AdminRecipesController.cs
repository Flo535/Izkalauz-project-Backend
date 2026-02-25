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

        [HttpGet("pending")]
        public async Task<IActionResult> GetPending()
        {
            try
            {
                // Szigorú szűrés: IsApproved értéke pontosan false
                var pending = await _context.Recipes
                    .Include(r => r.Ingredients)
                    .Where(r => r.IsApproved == false)
                    .OrderByDescending(r => r.CreatedAt)
                    .ToListAsync();

                return Ok(pending);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var all = await _context.Recipes
                .Include(r => r.Ingredients)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
            return Ok(all);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] StatusUpdateDto dto)
        {
            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == id);
            if (recipe == null) return NotFound();

            recipe.IsApproved = dto.IsApproved;
            recipe.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Státusz frissítve!" });
        }
    }

    public class StatusUpdateDto
    {
        public bool IsApproved { get; set; }
    }
}