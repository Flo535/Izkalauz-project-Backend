using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IzKalauzBackend.Models
{
    public class Recipe
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string HowToText { get; set; } = string.Empty;

        public string? ImagePath { get; set; }

        // Dani 5. pontja: A beküldött recept alapból nem jóváhagyott (false)
        public bool IsApproved { get; set; } = false;

        // Jogosultságkezelés: Ki a recept tulajdonosa?
        [Required]
        public string AuthorEmail { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Kapcsolat a hozzávalókkal
        public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}