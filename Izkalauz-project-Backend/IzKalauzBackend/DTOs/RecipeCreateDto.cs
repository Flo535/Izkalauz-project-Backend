using System.Collections.Generic;

namespace IzKalauzBackend.DTOs
{
    public class RecipeCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string HowToText { get; set; } = string.Empty;
        public List<string> Ingredients { get; set; } = new();
    }
}
