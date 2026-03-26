using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IzKalauzBackend.Models
{
    public class Ingredient
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public decimal Quantity { get; set; }
        
        [Required]
        public string Unit { get; set; } = string.Empty;

        // Foreign key a Recipe-hez
        public Guid RecipeId { get; set; }

        [JsonIgnore]
        public Recipe? Recipe { get; set; } = null!;
    }
}

