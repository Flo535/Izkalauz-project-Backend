using IzKalauzBackend.Models;

public class Recipe
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string HowToText { get; set; } = string.Empty;
    public string AuthorEmail { get; set; } = string.Empty;
    public string? ImagePath { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }


    // EF navigációs property
    public List<Ingredient> Ingredients { get; set; } = new();
}
