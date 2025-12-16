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
                        Ingredients = new List<string> { "csirke", "paprika", "hagymás tejföl", "só", "bors" },
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
                        Ingredients = new List<string> { "liszt", "tojás", "tej", "csokoládé", "cukor", "só" },
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
                        Ingredients = new List<string> { "liszt", "cukor", "vaj", "fahéj", "élesztő", "tej" },
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
                        Ingredients = new List<string> { "marhahús", "burgonya", "sárgarépa", "hagyma", "paprika", "só" },
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
                        Ingredients = new List<string> { "paradicsom", "kecskesajt", "bazsalikom", "olívaolaj", "só" },
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
                        Ingredients = new List<string> { "túró", "málna", "tojás", "cukor", "liszt", "vaj" },
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
                        Ingredients = new List<string> { "liszt", "tojás", "cukor", "csokoládé", "tej" },
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
                        Ingredients = new List<string> { "sertéshús", "sajt", "sonka", "tojás", "liszt", "só" },
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
                        Ingredients = new List<string> { "káposzta", "paradicsom", "olaj", "só", "bors" },
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
                        Ingredients = new List<string> { "spagetti", "paradicsom", "fokhagyma", "bazsalikom", "olívaolaj" },
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
                        Ingredients = new List<string> { "sertéskaraj", "bazsalikom", "parmezán", "fokhagyma", "olívaolaj" },
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
                        Ingredients = new List<string> { "liszt", "cukor", "tojás", "citrom", "vaj", "tej" },
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
                        Ingredients = new List<string> { "sertéshús", "pak choi", "rizs", "só", "bors", "olaj" },
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
                        Ingredients = new List<string> { "burgonya", "tojás", "kolbász", "tejföl", "só" },
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
                        Ingredients = new List<string> { "hús", "bab", "rizs", "hagymás", "paprika", "só" },
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
                        Ingredients = new List<string> { "darált hús", "tojás", "zsemlemorzsa", "só", "bors" },
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
                        Ingredients = new List<string> { "tészta", "túró", "tejföl", "szalonna", "só" },
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
                        Ingredients = new List<string> { "zöldborsó", "sárgarépa", "hagyma", "só", "bors" },
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
                        Ingredients = new List<string> { "sárgarépa", "zeller", "karalábé", "karfiol", "só", "petrezselyem" },
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
                        Ingredients = new List<string> { "liszt", "tojás", "cukor", "alma", "vaj" },
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "Images/Almás piskóta.jpg",
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
