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
                        Title = "Csirkepaprikás",
                        Category = "Leves",
                        Description = "Hagyományos magyar csirkepaprikás galuskával.",
                        HowToText = "Csirkepaprikás: HowTo",
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient { Id = Guid.NewGuid(), Name = "csirke", Quantity = 1.2m, Unit = Unit.All[2] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vöröshagyma", Quantity = 2, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "őrölt paprika", Quantity = 2, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tejföl", Quantity = 2, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "bors", Quantity = 1, Unit = Unit.All[10] }
                        },
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "Images/Csirkepaprikás.jpg",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Recipe
                    {
                        Id = Guid.NewGuid(),
                        Title = "Csokis palacsinta",
                        Category = "Desszert",
                        Description = "Finom palacsinta csokoládéöntettel.",
                        HowToText = "Csokis palacsinta: HowTo",
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient { Id = Guid.NewGuid(), Name = "liszt", Quantity = 20, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tej", Quantity = 3, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tojás", Quantity = 2, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "cukor", Quantity = 2, Unit = Unit.All[8] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "csokoládé", Quantity = 10, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] }
                        },
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "Images/Csokis palacsinta.jpg",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Recipe
                    {
                        Id = Guid.NewGuid(),
                        Title = "Fahéjas tekercs",
                        Category = "Desszert",
                        Description = "Puha, fahéjas tekercs édes töltelékkel.",
                        HowToText = "Fahéjas tekercs: HowTo",
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient { Id = Guid.NewGuid(), Name = "liszt", Quantity = 50, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(),  Name = "cukor", Quantity = 10, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vaj", Quantity = 10, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "fahéj", Quantity = 2, Unit = Unit.All[8] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "élesztő", Quantity = 2, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tej", Quantity = 2.5m, Unit = Unit.All[4] }
                        },
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "Images/Fahéjas tekercs.jpg",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Recipe
                    {
                        Id = Guid.NewGuid(),
                        Title = "Gulyásleves",
                        Category = "Leves",
                        Description = "Húsos, magyaros gulyásleves zöldségekkel.",
                        HowToText = "Gulyásleves: HowTo",
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient { Id = Guid.NewGuid(), Name = "marhahús", Quantity = 60, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "burgonya", Quantity = 40, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "sárgarépa", Quantity = 2, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vöröshagyma", Quantity = 1, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "őrölt paprika", Quantity = 1, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] }
                        },
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "Images/Gulyásleves.jpg",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Recipe
                    {
                        Id = Guid.NewGuid(),
                        Title = "Kecskesajtos paradicsom",
                        Category = "Főétel",
                        Description = "Friss paradicsom és kecskesajt saláta.",
                        HowToText = "Kecskesajtos paradicsom: HowTo",
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient { Id = Guid.NewGuid(), Name = "paradicsom", Quantity = 50, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "kecskesajt", Quantity = 15, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "bazsalikom", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "olívaolaj", Quantity = 2, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] }
                        },
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "Images/Kecskesajtos paradicsom.jpg",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Recipe
                    {
                        Id = Guid.NewGuid(),
                        Title = "Málnás túrótorta",
                        Category = "Desszert",
                        Description = "Krémes túrótorta friss málnával.",
                        HowToText = "Málnás túrótorta: HowTo",
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient { Id = Guid.NewGuid(), Name = "túró", Quantity = 50, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "málna", Quantity = 20, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tojás", Quantity = 3, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "cukor", Quantity = 15, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "liszt", Quantity = 10, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vaj", Quantity = 10, Unit = Unit.All[1] }
                        },
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "Images/Málnás túrótorta.jpg",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Recipe
                    {
                        Id = Guid.NewGuid(),
                        Title = "Menzás csokiöntetes piskóta",
                        Category = "Desszert",
                        Description = "Iskolai menzás ízű csokis piskóta.",
                        HowToText = "Menzás csokiöntetes piskóta: HowTo",
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient { Id = Guid.NewGuid(), Name = "liszt", Quantity = 30, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tojás", Quantity = 4, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "cukor", Quantity = 20, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "csokoládé", Quantity = 10, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tej", Quantity = 2, Unit = Unit.All[4] }
                        },
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "Images/Menzás csokiöntetes piskóta.jpg",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Recipe
                    {
                        Id = Guid.NewGuid(),
                        Title = "Óvári sertésszelet",
                        Category = "Főétel",
                        Description = "Sajttal és sonkával töltött rántott sertésszelet.",
                        HowToText = "Óvári sertésszelet: HowTo",
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient { Id = Guid.NewGuid(), Name = "sertéshús", Quantity = 60, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "sajt", Quantity = 15, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "sonka", Quantity = 10, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tojás", Quantity = 2, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "liszt", Quantity = 10, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] }
                        },
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "Images/Óvári sertésszelet.jpg",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Recipe
                    {
                        Id = Guid.NewGuid(),
                        Title = "Paradicsomos káposzta",
                        Category = "Főétel",
                        Description = "Egyszerű paradicsomos káposzta köretnek.",
                        HowToText = "Paradicsomos káposzta: HowTo",
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient { Id = Guid.NewGuid(), Name = "káposzta", Quantity = 80, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "paradicsom", Quantity = 30, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "olaj", Quantity = 2, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "bors", Quantity = 1, Unit = Unit.All[10] }
                        },
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "Images/Paradicsomos káposzta.jpg",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Recipe
                    {
                        Id = Guid.NewGuid(),
                        Title = "Paradicsomos tészta",
                        Category = "Főétel",
                        Description = "Egyszerű, olaszos tészta paradicsomszósszal.",
                        HowToText = "Paradicsomos tészta: HowTo",
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient { Id = Guid.NewGuid(), Name = "spagetti", Quantity = 40, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "paradicsom", Quantity = 30, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "fokhagyma", Quantity = 2, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "bazsalikom", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "olívaolaj", Quantity = 2, Unit = Unit.All[9] }
                        },
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "Images/Paradicsomos tészta.jpg",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Recipe
                    {
                        Id = Guid.NewGuid(),
                        Title = "Pestós karaj",
                        Category = "Főétel",
                        Description = "Sertéskaraj pestós öntettel sütve.",
                        HowToText = "Pestós karaj: HowTo",
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient { Id = Guid.NewGuid(), Name = "sertéskaraj", Quantity = 60, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "bazsalikom", Quantity = 2, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "parmezán", Quantity = 10, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "fokhagyma", Quantity = 2, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "olívaolaj", Quantity = 2, Unit = Unit.All[9] }
                        },
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "Images/Pestós karaj.jpg",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Recipe
                    {
                        Id = Guid.NewGuid(),
                        Title = "Pihe-puha citromos süti",
                        Category = "Desszert",
                        Description = "Puha, citromos süti tejszínhabbal.",
                        HowToText = "Pihe-puha citromos süti: HowTo",
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient { Id = Guid.NewGuid(), Name = "liszt", Quantity = 30, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "cukor", Quantity = 20, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tojás", Quantity = 3, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "citrom", Quantity = 1, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vaj", Quantity = 10, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tej", Quantity = 2, Unit = Unit.All[4] }
                        },
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "Images/Pihe-puha citromos süti.jpg",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Recipe
                    {
                        Id = Guid.NewGuid(),
                        Title = "Sertéshús pak choi-jal rizzsel",
                        Category = "Főétel",
                        Description = "Sertéshús wokban pak choi-jal és rizzsel.",
                        HowToText = "Sertéshús pak choi-jal rizzsel: HowTo",
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient { Id = Guid.NewGuid(), Name = "sertéshús", Quantity = 50, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "pak choi", Quantity = 2, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "rizs", Quantity = 25, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "bors", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "olaj", Quantity = 1, Unit = Unit.All[9] }
                        },
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "Images/Sertéshús pak choi-jal rizzsel.jpg",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Recipe
                    {
                        Id = Guid.NewGuid(),
                        Title = "Rakott krumpli",
                        Category = "Főétel",
                        Description = "Hagyományos rakott krumpli kolbásszal és tojással.",
                        HowToText = "Rakott krumpli: HowTo",
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient { Id = Guid.NewGuid(), Name = "burgonya", Quantity = 80, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tojás", Quantity = 4, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "kolbász", Quantity = 20, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tejföl", Quantity = 3, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] }
                        },
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "Images/Rakott krumpli.jpg",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Recipe
                    {
                        Id = Guid.NewGuid(),
                        Title = "Rizses-babos hús",
                        Category = "Főétel",
                        Description = "Pikáns húsos egytál rizses babbal.",
                        HowToText = "Rizses-babos hús: HowTo",
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient { Id = Guid.NewGuid(), Name = "hús", Quantity = 50, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "bab", Quantity = 30, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "rizs", Quantity = 20, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vöröshagyma", Quantity = 1, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "paprika", Quantity = 1, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] }
                        },
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "Images/Rizses-babos hús.jpg",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Recipe
                    {
                        Id = Guid.NewGuid(),
                        Title = "Tepsiben sült fasírt",
                        Category = "Főétel",
                        Description = "Szaftos, tepsiben sült fasírt körettel.",
                        HowToText = "Tepsiben sült fasírt: HowTo",
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient { Id = Guid.NewGuid(), Name = "darált hús", Quantity = 70, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tojás", Quantity = 2, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "zsemlemorzsa", Quantity = 10, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "bors", Quantity = 1, Unit = Unit.All[10] }
                        },
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "Images/Tepsiben sült fasírt.jpg",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Recipe
                    {
                        Id = Guid.NewGuid(),
                        Title = "Túróscsusza",
                        Category = "Desszert",
                        Description = "Hagyományos magyar túróscsusza tejföllel és szalonnával.",
                        HowToText = "Túróscsusza: HowTo",
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient { Id = Guid.NewGuid(), Name = "tészta", Quantity = 40, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "túró", Quantity = 30, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tejföl", Quantity = 2, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "szalonna", Quantity = 15, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] }
                        },
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "Images/Túróscsusza.jpg",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Recipe
                    {
                        Id = Guid.NewGuid(),
                        Title = "Zöldborsóleves",
                        Category = "Leves",
                        Description = "Egyszerű, friss zöldborsóleves.",
                        HowToText = "Zöldborsóleves: HowTo",
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient { Id = Guid.NewGuid(), Name = "zöldborsó", Quantity = 40, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "sárgarépa", Quantity = 2, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vöröshagyma", Quantity = 1, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "bors", Quantity = 1, Unit = Unit.All[10] }
                        },
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "Images/Zöldborsóleves.jpg",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Recipe
                    {
                        Id = Guid.NewGuid(),
                        Title = "Zöldségleves",
                        Category = "Leves",
                        Description = "Könnyű, tápláló zöldségleves.",
                        HowToText = "Zöldségleves: HowTo",
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient { Id = Guid.NewGuid(), Name = "sárgarépa", Quantity = 3, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "zeller", Quantity = 10, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "karalábé", Quantity = 1, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "karfiol", Quantity = 20, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "petrezselyem", Quantity = 1, Unit = Unit.All[10] }
                        },
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "Images/Zöldségleves.jpg",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Recipe
                    {
                        Id = Guid.NewGuid(),
                        Title = "Almás piskóta",
                        Category = "Desszert",
                        Description = "Puha almás piskóta szeletek.",
                        HowToText = "Almás piskóta: HowTo",
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient { Id = Guid.NewGuid(), Name = "liszt", Quantity = 30, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tojás", Quantity = 4, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "cukor", Quantity = 20, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "alma", Quantity = 3, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vaj", Quantity = 10, Unit = Unit.All[1] }
                        },
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "Images/Almás piskóta.jpg",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                };
                // Beállítjuk a RecipeId-t minden Ingredient-re
                foreach (var recipe in recipes)
                {
                    foreach (var ingredient in recipe.Ingredients)
                    {
                        ingredient.RecipeId = recipe.Id;
                    }
                }
                await context.Recipes.AddRangeAsync(recipes);
                await context.SaveChangesAsync();
            }
        }
    }
}
