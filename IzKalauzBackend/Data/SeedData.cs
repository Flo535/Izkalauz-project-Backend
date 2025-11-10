using IzKalauzBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace IzKalauzBackend.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(AppDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            // --- ADMIN FELHASZNÁLÓ ---
            if (!context.Users.Any(u => u.Email == "admin@izkalauz.hu"))
            {
                var admin = new User
                {
                    Id = Guid.NewGuid(),
                    Email = "admin@izkalauz.hu",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = "Admin"
                };

                context.Users.Add(admin);
                await context.SaveChangesAsync();
            }

            // --- RECEPTEK ---
            if (!context.Recipes.Any())
            {
                var recipes = new List<Recipe>
                {
                    new Recipe
                    {
                        Id = Guid.NewGuid(),
                        Title = "Magyaros zöldségleves",
                        Description = "Könnyű, tápláló zöldségleves sárgarépával, zellerrel és karfiollal.",
                        Ingredients = new List<string> { "sárgarépa", "zeller", "karalábé", "karfiol", "só", "petrezselyem" },
                        AuthorEmail = "admin@izkalauz.hu",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Recipe
                    {
                        Id = Guid.NewGuid(),
                        Title = "Paradicsomos tészta",
                        Description = "Egyszerű olaszos tészta paradicsomszósszal és bazsalikommal.",
                        Ingredients = new List<string> { "spagetti", "paradicsom", "fokhagyma", "bazsalikom", "olívaolaj", "só" },
                        AuthorEmail = "admin@izkalauz.hu",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Recipe
                    {
                        Id = Guid.NewGuid(),
                        Title = "Rakott krumpli",
                        Description = "Hagyományos magyar rakott krumpli kolbásszal és tejföllel.",
                        Ingredients = new List<string> { "burgonya", "tojás", "kolbász", "tejföl", "só", "vaj" },
                        AuthorEmail = "admin@izkalauz.hu",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                };

                await context.Recipes.AddRangeAsync(recipes);
                await context.SaveChangesAsync();
            }
        }
    }
}
