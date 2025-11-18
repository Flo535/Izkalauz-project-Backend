using IzKalauzBackend.Data;
using IzKalauzBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IzKalauzBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // DTO a felhasználói adatokhoz (jelszó nélkül)
        public class UserDto
        {
            public Guid Id { get; set; }
            public string Email { get; set; } = null!;
            public string Role { get; set; } = "User";
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _context.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    Role = u.Role
                })
                .ToListAsync();

            return Ok(users);
        }

        // GET: api/Users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {
            var user = await _context.Users
                .Where(u => u.Id == id)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    Role = u.Role
                })
                .FirstOrDefaultAsync();

            if (user == null) return NotFound();

            return Ok(user);
        }

        // DELETE: api/Users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Users/{id}/role
        [HttpPut("{id}/role")]
        public async Task<IActionResult> UpdateUserRole(Guid id, [FromBody] string newRole)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            if (newRole != "User" && newRole != "Admin")
                return BadRequest("Role must be 'User' or 'Admin'.");

            user.Role = newRole;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Segédfüggvény a jelenlegi admin email lekérdezéséhez (opcionális)
        private string? GetCurrentUserEmail()
        {
            return User?.Identity?.IsAuthenticated == true
                ? User.Identity.Name
                : null;
        }
    }
}
