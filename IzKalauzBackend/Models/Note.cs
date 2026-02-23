using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace IzKalauzBackend.Models
{
    // Ez az index biztosítja, hogy egy emailhez csak egyetlen jegyzet sor tartozhasson az adatbázisban
    [Index(nameof(AuthorEmail), IsUnique = true)]
    public class Note
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        public string AuthorEmail { get; set; } = string.Empty;

        // Visszatettük a 300-as limitet modell szinten is a validációhoz
        [MaxLength(300, ErrorMessage = "A jegyzet hossza nem haladhatja meg a 300 karaktert!")]
        public string Text { get; set; } = string.Empty;

        // Opcionális: hozzáadtunk egy frissítési időpontot, hogy lássuk, mikori a jegyzet
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}