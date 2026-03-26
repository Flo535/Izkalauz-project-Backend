using System.ComponentModel.DataAnnotations;

namespace IzKalauzBackend.Models
{
    public class FavoriteRecipe
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        [Required]
        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null!;

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
