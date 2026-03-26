using System.Security.Claims;
using IzKalauzBackend.Data;
using IzKalauzBackend.DTOs;
using IzKalauzBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IzKalauzBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NotesController(AppDbContext context)
        {
            _context = context;
        }

        // Segédmetódus: Biztonságosan kinyeri az email címet a tokenből, többféle formátumot is kezelve
        private string? GetUserEmail()
        {
            return User.FindFirst(ClaimTypes.Email)?.Value
                ?? User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value
                ?? User.Claims.FirstOrDefault(c => c.Type.Contains("email"))?.Value;
        }

        // ==========================================
        // GET: api/Notes/mine
        // Jegyzet lekérése vagy üres létrehozása
        // ==========================================
        [HttpGet("mine")]
        public async Task<IActionResult> GetMyNote()
        {
            try
            {
                var userEmail = GetUserEmail();
                if (string.IsNullOrEmpty(userEmail))
                {
                    return Unauthorized(new { message = "A token nem tartalmaz érvényes email címet!" });
                }

                var note = await _context.Notes
                    .FirstOrDefaultAsync(n => n.AuthorEmail == userEmail);

                // Ha még nincs jegyzete az adatbázisban, létrehozunk egy alapértelmezettet
                if (note == null)
                {
                    note = new Note
                    {
                        Id = Guid.NewGuid(),
                        AuthorEmail = userEmail,
                        Text = string.Empty,
                        UpdatedAt = DateTime.UtcNow
                    };
                    _context.Notes.Add(note);
                    await _context.SaveChangesAsync();
                }

                return Ok(note);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Hiba a lekérés során", error = ex.Message });
            }
        }

        // ==========================================
        // PUT: api/Notes/mine
        // Jegyzet mentése / frissítése
        // ==========================================
        [HttpPut("mine")]
        public async Task<IActionResult> UpdateMyNote([FromBody] NoteDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var userEmail = GetUserEmail();
                if (string.IsNullOrEmpty(userEmail)) return Unauthorized();

                // Megkeressük a meglévő jegyzetet az egyedi email alapján
                var note = await _context.Notes
                    .FirstOrDefaultAsync(n => n.AuthorEmail == userEmail);

                if (note == null)
                {
                    // Ha valamiért nem létezett volna (pl. manuális törlés után), létrehozzuk
                    note = new Note
                    {
                        Id = Guid.NewGuid(),
                        AuthorEmail = userEmail,
                        Text = dto.Text ?? string.Empty,
                        UpdatedAt = DateTime.UtcNow
                    };
                    _context.Notes.Add(note);
                }
                else
                {
                    // Frissítjük a szöveget és az időpontot
                    // A limitet a NoteDto [StringLength] attribútuma és a Note modell [MaxLength]-je is védi
                    note.Text = dto.Text ?? string.Empty;
                    note.UpdatedAt = DateTime.UtcNow;

                    _context.Entry(note).State = EntityState.Modified;
                }

                await _context.SaveChangesAsync();
                return Ok(note);
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new { message = "Adatbázis hiba: Az email címnek egyedinek kell lennie." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Szerver hiba a mentés során", error = ex.Message });
            }
        }

        // ==========================================
        // ADMIN FUNKCIÓ: Összes jegyzet megtekintése
        // ==========================================
        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllNotes()
        {
            var notes = await _context.Notes.ToListAsync();
            return Ok(notes);
        }
    }
}