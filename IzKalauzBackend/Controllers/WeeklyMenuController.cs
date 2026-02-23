using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IzKalauzBackend.Data;
using IzKalauzBackend.DTOs;
using IzKalauzBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IzKalauzBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeeklyMenuController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public WeeklyMenuController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ==========================================
        // GET: api/WeeklyMenu (BÁRKI láthatja)
        // ==========================================
        [HttpGet]
        [AllowAnonymous] // Biztosítja, hogy bejelentkezés nélkül is menjen
        public async Task<ActionResult<IEnumerable<WeeklyMenuDto>>> GetWeeklyMenu()
        {
            try
            {
                var items = await _context.WeeklyMenuItems
                    .Include(w => w.Soup)
                    .Include(w => w.MainCourse)
                    .Include(w => w.Dessert)
                    .OrderBy(w => w.DayOfWeek)
                    .ToListAsync();

                if (items == null) return Ok(new List<WeeklyMenuDto>());

                var dtos = _mapper.Map<List<WeeklyMenuDto>>(items);
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                // Ha 500-as hiba van, itt kiírja a pontos okot a konzolra
                return StatusCode(500, $"Belső szerverhiba: {ex.Message}");
            }
        }

        // ==========================================
        // POST: api/WeeklyMenu (Csak ADMIN)
        // ==========================================
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<WeeklyMenuDto>> CreateWeeklyMenuItem(CreateWeeklyMenuItemDto dto)
        {
            try
            {
                // Ellenőrzés, hogy az adott napra már van-e menü
                var existing = await _context.WeeklyMenuItems
                    .AnyAsync(w => w.DayOfWeek == (DayOfWeek)dto.DayOfWeek);

                if (existing)
                    return BadRequest("Erre a napra már van létrehozott menü.");

                var item = new WeeklyMenuItem
                {
                    Id = Guid.NewGuid(),
                    DayOfWeek = (DayOfWeek)dto.DayOfWeek,
                    SoupId = dto.SoupId,
                    MainCourseId = dto.MainCourseId,
                    DessertId = dto.DessertId
                };

                _context.WeeklyMenuItems.Add(item);
                await _context.SaveChangesAsync();

                // Újra lekérjük az Include-olt adatokkal a válaszhoz
                var result = await _context.WeeklyMenuItems
                    .Include(w => w.Soup)
                    .Include(w => w.MainCourse)
                    .Include(w => w.Dessert)
                    .FirstOrDefaultAsync(w => w.Id == item.Id);

                return CreatedAtAction(nameof(GetWeeklyMenu), _mapper.Map<WeeklyMenuDto>(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hiba a mentés során: {ex.Message}");
            }
        }

        // ==========================================
        // DELETE: api/WeeklyMenu/{id} (Csak ADMIN)
        // ==========================================
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteWeeklyMenuItem(Guid id)
        {
            var item = await _context.WeeklyMenuItems.FindAsync(id);
            if (item == null) return NotFound("Az elem nem létezik!");

            _context.WeeklyMenuItems.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}