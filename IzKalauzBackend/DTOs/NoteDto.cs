using System.ComponentModel.DataAnnotations;

namespace IzKalauzBackend.DTOs
{
    public class NoteDto
    {
        [StringLength(300, ErrorMessage = "A jegyzet nem lehet hosszabb 300 karakternél!")]
        // A kérdőjel (?) jelzi, hogy érkezhet üres/null érték is, 
        // amit a Controllerben majd lekezelünk
        public string? Text { get; set; } = string.Empty;
    }
}