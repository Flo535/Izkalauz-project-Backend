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

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<WeeklyMenuDto>>> GetWeeklyMenu()
        {
            try
            {
                // Itt dől el minden: Fontos az Include, hogy ne legyen null a válasz!
                var items = await _context.WeeklyMenuItems
                    .Include(w => w.Soup)
                    .Include(w => w.MainCourse)
                    .Include(w => w.Dessert)
                    .OrderBy(w => w.DayOfWeek)
                    .ToListAsync();

                // Ha üres, ne hibát dobjon, hanem üres listát
                if (items == null || !items.Any())
                    return Ok(new List<WeeklyMenuDto>());

                var dtos = _mapper.Map<List<WeeklyMenuDto>>(items);
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                // Logold a belső hiba részleteit is!
                return StatusCode(500, $"Belső szerverhiba: {ex.Message} -> {ex.InnerException?.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<WeeklyMenuDto>> CreateWeeklyMenuItem(CreateWeeklyMenuItemDto dto)
        {
            try
            {
                // 1. Töröljük a régi menüt arra a napra, ha már létezik (Felülírás logika)
                var existing = await _context.WeeklyMenuItems
                    .FirstOrDefaultAsync(w => w.DayOfWeek == (DayOfWeek)dto.DayOfWeek);

                if (existing != null)
                {
                    _context.WeeklyMenuItems.Remove(existing);
                }

                // 2. Új elem létrehozása
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

                // 3. Visszaadjuk a teljes objektumot a nevekkel együtt
                var result = await _context.WeeklyMenuItems
                    .Include(w => w.Soup)
                    .Include(w => w.MainCourse)
                    .Include(w => w.Dessert)
                    .FirstOrDefaultAsync(w => w.Id == item.Id);

                return Ok(_mapper.Map<WeeklyMenuDto>(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hiba a mentés során: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteWeeklyMenuItem(Guid id)
        {
            var item = await _context.WeeklyMenuItems.FindAsync(id);
            if (item == null) return NotFound();

            _context.WeeklyMenuItems.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}