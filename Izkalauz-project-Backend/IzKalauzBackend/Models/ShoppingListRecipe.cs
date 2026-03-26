using System.ComponentModel.DataAnnotations;

namespace IzKalauzBackend.Models
{
    public class ShoppingListRecipe
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ShoppingListId { get; set; }
        public ShoppingList ShoppingList { get; set; } = null!;

        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null!;

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
