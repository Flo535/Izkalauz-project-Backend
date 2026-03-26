using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IzKalauzBackend.Models
{
    public class ShoppingListItem
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public string Unit { get; set; } = string.Empty;

        public Guid ShoppingListId { get; set; }

        [JsonIgnore]
        public ShoppingList ShoppingList { get; set; } = null!;
    }
}