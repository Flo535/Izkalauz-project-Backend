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

            // --- ALAP 20 RECEPTEK ---
            var baseRecipes = new List<Recipe>
            {
                new Recipe { Title = "Csirkepaprikás", Description = "Egyszerű magyar csirkepaprikás tejfölös szósszal.", Ingredients = new List<string>{"csirke","paprika","hagyma","tejföl","só","bors"}, AuthorEmail="admin@izkalauz.hu" },
                new Recipe { Title = "Csokis palacsinta", Description = "Finom csokis palacsinta csokoládéval.", Ingredients = new List<string>{"liszt","tojás","tej","csokoládé","cukor"}, AuthorEmail="admin@izkalauz.hu" },
                new Recipe { Title = "Fahéjas tekercs", Description = "Illatos fahéjas tekercs reggelire.", Ingredients = new List<string>{"liszt","élesztő","cukor","fahéj","vaj"}, AuthorEmail="admin@izkalauz.hu" },
                new Recipe { Title = "Gulyásleves", Description = "Hagyományos magyar gulyásleves.", Ingredients = new List<string>{"marhahús","sárgarépa","burgonya","paprika","hagyma","fűszerek"}, AuthorEmail="admin@izkalauz.hu" },
                new Recipe { Title = "Kecskesajtos paradicsom", Description = "Friss paradicsom kecskesajttal.", Ingredients = new List<string>{"paradicsom","kecskesajt","bazsalikom","olívaolaj"}, AuthorEmail="admin@izkalauz.hu" },
                new Recipe { Title = "Málnás túrótorta", Description = "Krémes túrótorta málnával.", Ingredients = new List<string>{"túró","málna","cukor","tojás","liszt"}, AuthorEmail="admin@izkalauz.hu" },
                new Recipe { Title = "Menzás csokiöntetes piskóta", Description = "Gyors csokis piskóta a gyerekeknek.", Ingredients = new List<string>{"liszt","tojás","cukor","csokoládé","tej"}, AuthorEmail="admin@izkalauz.hu" },
                new Recipe { Title = "Óvári sertésszelet", Description = "Ropogós óvári sertésszelet sajttal és sonkával.", Ingredients = new List<string>{"sertéshús","sajt","sonka","liszt","tojás"}, AuthorEmail="admin@izkalauz.hu" },
                new Recipe { Title = "Paradicsomos káposzta", Description = "Sütőben sült káposzta paradicsommal.", Ingredients = new List<string>{"káposzta","paradicsom","olívaolaj","fokhagyma"}, AuthorEmail="admin@izkalauz.hu" },
                new Recipe { Title = "Paradicsomos tészta", Description = "Egyszerű paradicsomos tészta.", Ingredients = new List<string>{"tészta","paradicsom","fokhagyma","bazsalikom","olívaolaj"}, AuthorEmail="admin@izkalauz.hu" },
                new Recipe { Title = "Pestós karaj", Description = "Szaftos karaj pestóval sütve.", Ingredients = new List<string>{"sertéshús","pesto","só","bors"}, AuthorEmail="admin@izkalauz.hu" },
                new Recipe { Title = "Pihe-puha citromos süti", Description = "Frissítő citromos süti.", Ingredients = new List<string>{"liszt","cukor","tojás","citrom","vaj"}, AuthorEmail="admin@izkalauz.hu" },
                new Recipe { Title = "Sertéshús pak choi-jal rizzsel", Description = "Ázsiai stílusú sertéshús zöldségekkel.", Ingredients = new List<string>{"sertéshús","pak choi","rizs","szójaszósz"}, AuthorEmail="admin@izkalauz.hu" },
                new Recipe { Title = "Rakott krumpli", Description = "Hagyományos rakott krumpli kolbásszal és tejföllel.", Ingredients = new List<string>{"burgonya","tojás","kolbász","tejföl","só","vaj"}, AuthorEmail="admin@izkalauz.hu" },
                new Recipe { Title = "Rizses-babos hús", Description = "Egyszerű egytálétel rizs és bab hozzáadásával.", Ingredients = new List<string>{"hús","rizs","bab","paradicsom","fűszerek"}, AuthorEmail="admin@izkalauz.hu" },
                new Recipe { Title = "Tepsiben sült fasírt", Description = "Szaftos fasírt tepsiben sütve.", Ingredients = new List<string>{"darált hús","tojás","zsemlemorzsa","fűszerek"}, AuthorEmail="admin@izkalauz.hu" },
                new Recipe { Title = "Túróscsusza", Description = "Hagyományos túróscsusza.", Ingredients = new List<string>{"csusza tészta","túró","tejföl","só"}, AuthorEmail="admin@izkalauz.hu" },
                new Recipe { Title = "Zöldborsóleves", Description = "Egyszerű zöldborsóleves.", Ingredients = new List<string>{"zöldborsó","hagyma","sárgarépa","só","bors"}, AuthorEmail="admin@izkalauz.hu" },
                new Recipe { Title = "Zöldségleves", Description = "Vegyes zöldségleves.", Ingredients = new List<string>{"sárgarépa","burgonya","zeller","hagyma","só","bors"}, AuthorEmail="admin@izkalauz.hu" },
                new Recipe { Title = "Almás piskóta", Description = "Puha almás piskóta.", Ingredients = new List<string>{"liszt","cukor","tojás","alma","vaj"}, AuthorEmail="admin@izkalauz.hu" }
            };

            foreach (var recipe in baseRecipes)
            {
                // Ellenőrizzük, van-e már ilyen admin recept
                var existing = context.Recipes
                                      .AsNoTracking()
                                      .FirstOrDefault(r => r.AuthorEmail == "admin@izkalauz.hu" && r.Title == recipe.Title);

                if (existing != null)
                {
                    // Frissítés: konkrét értékek beállítása
                    existing.Description = recipe.Description;
                    existing.Ingredients = recipe.Ingredients;
                    existing.UpdatedAt = DateTime.UtcNow;

                    context.Recipes.Update(existing);
                }
                else
                {
                    // Új recept hozzáadása
                    recipe.Id = Guid.NewGuid();
                    recipe.CreatedAt = DateTime.UtcNow;
                    recipe.UpdatedAt = DateTime.UtcNow;
                    await context.Recipes.AddAsync(recipe);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
