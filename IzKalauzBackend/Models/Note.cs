using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace IzKalauzBackend.Models
{
    [Index(nameof(AuthorEmail), IsUnique = true)]
    public class Note
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        public string AuthorEmail { get; set; } = string.Empty;

        [MaxLength(300)]
        public string Text { get; set; } = string.Empty;
    }
}