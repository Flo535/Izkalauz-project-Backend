using System;
using System.Collections.Generic;

namespace IzKalauzBackend.DTOs
{
    public class RecipeReadDto
    {
        public Guid Id { get; set; }
        public string Nev { get; set; } = string.Empty;
        public string Leiras { get; set; } = string.Empty;
        public List<string> Hozzavalok { get; set; } = new();
        public string CreatedByEmail { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
