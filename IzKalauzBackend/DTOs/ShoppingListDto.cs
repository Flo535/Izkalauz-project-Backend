using IzKalauzBackend.Models;

namespace IzKalauzBackend.DTOs
{
    public class ShoppingListDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<ShoppingListItemDto> Items { get; set; } = new();
        public List<RecipeDto> Recipes { get; set; } = new();
    }

    public class ShoppingListItemDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
    }
}
