using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IzKalauzBackend.Models
{
    public class Recipe
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "A cím kötelező!")]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "A kategória kötelező!")]
        public string Category { get; set; } = string.Empty;

        public string? Description { get; set; }
        public string? HowToText { get; set; }
        public string? ImagePath { get; set; }

        public bool IsApproved { get; set; } = false;

        // Kivettük a Required attribútumot, a kontroller tölti ki
        public string AuthorEmail { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}