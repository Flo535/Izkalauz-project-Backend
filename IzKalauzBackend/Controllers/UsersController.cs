using IzKalauzBackend.Data;
using IzKalauzBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IzKalauzBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] // Csak Admin érheti el az egész kontrollert
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        public class UserDto
        {
            public Guid Id { get; set; }
            public string Email { get; set; } = null!;
            public string Role { get; set; } = "User";
            public int RecipeCount { get; set; }
        }

        public class UpdateRoleDto
        {
            public string Role { get; set; } = "User";
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _context.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    Role = u.Role,
                    RecipeCount = _context.Recipes.Count(r => r.AuthorEmail == u.Email)
                })
                .ToListAsync();

            return Ok(users);
        }

        [HttpPut("{id}/role")]
        public async Task<IActionResult> UpdateUserRole(Guid id, [FromBody] UpdateRoleDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            if (dto.Role != "User" && dto.Role != "Admin")
                return BadRequest("A szerepkör csak 'User' vagy 'Admin' lehet.");

            user.Role = dto.Role;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}