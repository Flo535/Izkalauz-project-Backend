using System;
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

        // GET: api/weeklymenu      (bárki láthatja)
        [HttpGet]
        [AllowAnonymous] // <<--- EZ A FONTOS! Engedélyezi anonímnak is
        public async Task<ActionResult<IEnumerable<WeeklyMenuDto>>> GetWeeklyMenu()
        {
            var items = await _context.WeeklyMenuItems
                .Include(w => w.Soup)
                .Include(w => w.MainCourse)
                .Include(w => w.Dessert)
                .OrderBy(w => w.DayOfWeek)
                .ToListAsync();

            var dtos = _mapper.Map<List<WeeklyMenuDto>>(items);
            return Ok(dtos);
        }

        // POST: api/weeklymenu     (csak Admin)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<WeeklyMenuDto>> CreateWeeklyMenuItem(CreateWeeklyMenuItemDto dto)
        {
            // Ellenőrzés, hogy az adott napra már van-e
            if (_context.WeeklyMenuItems.Any(w => w.DayOfWeek == (DayOfWeek)dto.DayOfWeek))
                return BadRequest("Erre a napra már van létrehozott menü.");

            var item = new WeeklyMenuItem
            {
                DayOfWeek = (DayOfWeek)dto.DayOfWeek,
                SoupId = dto.SoupId,
                MainCourseId = dto.MainCourseId,
                DessertId = dto.DessertId
            };

            _context.WeeklyMenuItems.Add(item);
            await _context.SaveChangesAsync();

            var result = await _context.WeeklyMenuItems
                .Include(w => w.Soup).Include(w => w.MainCourse).Include(w => w.Dessert)
                .FirstAsync(w => w.Id == item.Id);

            return CreatedAtAction(nameof(GetWeeklyMenu), _mapper.Map<WeeklyMenuDto>(result));
        }

        // DELETE: api/weeklymenu/5 → csak Admin
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
