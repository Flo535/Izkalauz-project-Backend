namespace IzKalauzBackend.DTOs
{
    public class WeeklyRecipeDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Category { get; set; } = string.Empty;
        public string? ImageURL { get; set; }
    }
}
