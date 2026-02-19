using System.ComponentModel.DataAnnotations;

namespace IzKalauzBackend.DTOs
{
    public class NoteDto
    {
        [StringLength(300, ErrorMessage = "Maximum 300 karakter!")]
        public string Text { get; set; } = string.Empty;
    }
}
