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

        // GET: api/mynote
        //Bejelentkezett felhasználó lekéri a saját jegyzetét
        [HttpGet("mine")]
        public async Task<IActionResult> GetMyNote()
        {
            //User emailcím kiolvasása JWT tokenből
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized("A token nem tartalmaz email címet!");
            }

            // Felhasználóhoz tartozó jegyzet email alapján
            var note = await _context.Notes
                .FirstOrDefaultAsync(n => n.AuthorEmail == userEmail);
            if (note == null)
            {
                // Jegyzet létrehozása, ha még nincs
                note = new Note
                {
                    Id = Guid.NewGuid(),
                    AuthorEmail = userEmail,
                    Text = string.Empty
                };
                _context.Notes.Add(note);
                await _context.SaveChangesAsync();
            }

            return Ok(note);
        }


        // PUT: api/mynote
        [HttpPut("mine")]
        public async Task<IActionResult> UpdateMyNote(NoteDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //User emailcím kiolvasása JWT tokenből
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized("A token nem tartalmaz érvényes email címet!");
            }

            // Felhasználóhoz tartozó jegyzet email alapján
            var note = await _context.Notes
                .FirstOrDefaultAsync(n => n.AuthorEmail == userEmail);
            if (note == null)
            {
                return NotFound("Nem található jegyzet ehhez a felhasználóhoz!");
            }

            // Jegyzet frissítése
            note.Text = dto.Text;
            await _context.SaveChangesAsync();
            return Ok(note);
        }

        // GET api/allnotes
        // Adminoknak! Összes jegyzet lekérdezése
        [Authorize(Roles  = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllNotes()
        {
            var notes = await _context.Notes
                .Select(n => new AdminAllNotesDto
                {
                    AuthorEmail = n.AuthorEmail,
                    Text = n.Text
                })
                .ToListAsync();

            return Ok(notes);
        }

        // GET api/note/id
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNote(Guid id)
        {
            var item = await _context.Notes.FindAsync(id);
            if (item == null)
            {
                return NotFound("Az elem nem létezik!");
            }

            return Ok(item);
        }
    }
}
