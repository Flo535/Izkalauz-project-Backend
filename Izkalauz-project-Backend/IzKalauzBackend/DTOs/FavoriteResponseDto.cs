namespace IzKalauzBackend.Models.DTOs
{
    public class FavoriteResponseDto
    {
        public Guid Id { get; set; }
        public Guid RecipeId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImagePath { get; set; }
        public DateTime AddedAt { get; set; }
    }
}