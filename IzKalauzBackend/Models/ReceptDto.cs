using System;
using System.Collections.Generic;

namespace IzKalauzBackend.Models
{
    public class RecipeDto
    {
        public string Nev { get; set; } = string.Empty;
        public string Leiras { get; set; } = string.Empty;
        public List<string> Hozzavalok { get; set; } = new();
        public string CreatedByEmail { get; set; } = string.Empty;
    }
}
