using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IzKalauzBackend.Models
{
    public class CreateRecipeRequest
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public List<string> Ingredients { get; set; } = new();

        public List<string> Tags { get; set; } = new();

        public string Category { get; set; } = string.Empty;
    }
}
