using System.ComponentModel.DataAnnotations;

namespace IzKalauzBackend.Models
{
    public class ShoppingList
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string AuthorEmail { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<ShoppingListItem> Items { get; set; } = new();

        public List<ShoppingListRecipe> Recipes { get; set; } = new();
    }
}
