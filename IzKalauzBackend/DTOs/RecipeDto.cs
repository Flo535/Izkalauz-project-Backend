using System;
using System.Collections.Generic;

namespace IzKalauzBackend.Models
{
    public class RecipeDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }
}
