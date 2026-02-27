using IzKalauzBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace IzKalauzBackend.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(AppDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            // --- FELHASZNÁLÓK (Admin, Anna, Nono) ---
            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Email = "admin@izkalauz.hu",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                        Role = "Admin"
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Email = "anna@izkalauz.hu",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("jelszo123"),
                        Role = "User"
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Email = "nono@izkalauz.hu",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("jelszo123"),
                        Role = "User"
                    }
                };
                context.Users.AddRange(users);
                await context.SaveChangesAsync();
            }

            // --- RECEPTEK ÉS HOZZÁVALÓK ---
            if (!context.Recipes.Any())
            {
                var recipes = new List<Recipe>
                {
                    // --- EREDETI ALAPRECEPTEK ---
                    new Recipe { Id = Guid.NewGuid(), Title = "Csirkepaprikás", Category = "Főétel", Description = "Klasszikus magyar fogás szaftosan.", HowToText = "A hagymát pirítsuk meg, adjuk hozzá a húst és a paprikát, majd tejföllel habarjuk.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "csirkep_aprikas.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "csirkecomb", Quantity = 1, Unit = "kg" }, new Ingredient { Id = Guid.NewGuid(), Name = "tejföl", Quantity = 200, Unit = "ml" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Gulyásleves", Category = "Leves", Description = "Hagyományos marhagulyás.", HowToText = "A marhahúst pörkölt alapon indítjuk, majd zöldségekkel és vízzel felöntve készre főzzük.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "gulyas_leves.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "marhahús", Quantity = 0.6m, Unit = "kg" }, new Ingredient { Id = Guid.NewGuid(), Name = "burgonya", Quantity = 0.5m, Unit = "kg" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Pestos karaj", Category = "Főétel", Description = "Olaszos ízvilágú sült hús.", HowToText = "A karajszeleteket megkenjük pestoval és tepsiben összesütjük.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "pestos_karaj.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "sertéskaraj", Quantity = 4, Unit = "szelet" }, new Ingredient { Id = Guid.NewGuid(), Name = "bazsalikom pesto", Quantity = 100, Unit = "g" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Citromos süti", Category = "Desszert", Description = "Frissítő és puha édesség.", HowToText = "A hozzávalókat összekeverjük, citromhéjat reszelünk bele és tűpróbáig sütjük.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "pihe-puha_citromos_suti.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "liszt", Quantity = 250, Unit = "g" }, new Ingredient { Id = Guid.NewGuid(), Name = "citrom", Quantity = 2, Unit = "db" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Rakott krumpli", Category = "Főétel", Description = "Sok kolbásszal és tojással.", HowToText = "A főtt krumplit, tojást és kolbászt rétegezzük, majd sok tejföllel leöntve kisütjük.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "rakott_krumpli.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "krumpli", Quantity = 1, Unit = "kg" }, new Ingredient { Id = Guid.NewGuid(), Name = "kolbász", Quantity = 20, Unit = "dkg" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Túróscsusza", Category = "Főétel", Description = "Házi túróval és pörccel.", HowToText = "A tésztát kifőzzük, összekeverjük a túróval és tejföllel, a tetejére szalonnapörcöt teszünk.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "turoscsusza.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "csusztatészta", Quantity = 500, Unit = "g" }, new Ingredient { Id = Guid.NewGuid(), Name = "túró", Quantity = 250, Unit = "g" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Paradicsomos tészta", Category = "Főétel", Description = "Gyors és egyszerű ebéd.", HowToText = "A tésztát kifőzzük, a szószt bazsalikommal összefőzzük.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "paradicsomos_teszta.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "spagetti", Quantity = 500, Unit = "g" }, new Ingredient { Id = Guid.NewGuid(), Name = "paradicsomszósz", Quantity = 400, Unit = "ml" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Krémleves", Category = "Leves", Description = "Selymes zöldségkrémleves.", HowToText = "A zöldségeket puhára főzzük, majd botmixerrel pürésítjük.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "krem_leves.webp", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "vegyes zöldség", Quantity = 0.5m, Unit = "kg" }, new Ingredient { Id = Guid.NewGuid(), Name = "tejszín", Quantity = 200, Unit = "ml" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Almás piskóta", Category = "Desszert", Description = "Klasszikus nagymama-féle süti.", HowToText = "A piskótatésztába almát szeletelünk és fahéjjal megszórjuk.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "almas_piskota.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "alma", Quantity = 3, Unit = "db" }, new Ingredient { Id = Guid.NewGuid(), Name = "tojás", Quantity = 6, Unit = "db" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Málnás túrótorta", Category = "Desszert", Description = "Sütés nélküli finomság.", HowToText = "A kekszes alapra ráöntjük a túrókrémet és málnával díszítjük.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "malnas_turotorta.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "túró", Quantity = 500, Unit = "g" }, new Ingredient { Id = Guid.NewGuid(), Name = "málna", Quantity = 200, Unit = "g" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Óvári sertés", Category = "Főétel", Description = "Gombával, sonkával, sajttal.", HowToText = "A húst elősütjük, rátesszük a feltéteket és ráolvasztjuk a sajtot.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "ovari_sertesszelet.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "sertésszelet", Quantity = 4, Unit = "db" }, new Ingredient { Id = Guid.NewGuid(), Name = "gomba", Quantity = 200, Unit = "g" } } },

                    // --- ANNA ÉS NONO RECEPTJEI ---
                    new Recipe { Id = Guid.NewGuid(), Title = "Paradicsomos egytálétel", Category = "Főétel", Description = "Sült feta sajtos és koktélparadicsomos finomság.", HowToText = "A paradicsomokat és a fetát egy tepsibe tesszük, olívaolajjal és fokhagymával fűszerezzük.", AuthorEmail = "anna@izkalauz.hu", ImagePath = "paradicsomos_egytal.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "koktélparadicsom", Quantity = 40, Unit = "dkg" }, new Ingredient { Id = Guid.NewGuid(), Name = "feta sajt", Quantity = 200, Unit = "g" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Klasszikus vadas", Category = "Főétel", Description = "Krémes zöldségmártás puha hússal.", HowToText = "A zöldségeket megpároljuk, pürésítjük, mustárral és tejföllel ízesítjük.", AuthorEmail = "nono@izkalauz.hu", ImagePath = "vadas.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "sertéscomb", Quantity = 0.6m, Unit = "kg" }, new Ingredient { Id = Guid.NewGuid(), Name = "sárgarépa", Quantity = 3, Unit = "db" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Meggymártás", Category = "Desszert", Description = "Édes-savanykás gyümölcsmártás.", HowToText = "A meggyet felfőzzük, cukorral ízesítjük, majd lisztes habarással besűrítjük.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "meggymartas.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "meggy", Quantity = 500, Unit = "g" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Céklakrémleves", Category = "Leves", Description = "Élénk színű, vitaminban gazdag krémleves.", HowToText = "A céklát hagymával megpároljuk, puhára főzzük és pürésítjük.", AuthorEmail = "anna@izkalauz.hu", ImagePath = "ceklakremleves.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "cékla", Quantity = 0.5m, Unit = "kg" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Hagymakrémleves", Category = "Leves", Description = "Selymes leves sok-sok hagymával.", HowToText = "A hagymát vajon üvegesre pároljuk, alaplével felöntjük és tejszínnel dúsítjuk.", AuthorEmail = "nono@izkalauz.hu", ImagePath = "hagymakremleves.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "vöröshagyma", Quantity = 4, Unit = "db" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Epres szelet", Category = "Desszert", Description = "Gyors, sütés nélküli epres édesség.", HowToText = "A babapiskótát rétegezzük krémmel és friss eperszeletekkel.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "epres_szelet.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "eper", Quantity = 300, Unit = "g" }, new Ingredient { Id = Guid.NewGuid(), Name = "babapiskóta", Quantity = 1, Unit = "csomag" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Lencse leves", Category = "Leves", Description = "Hagyományos lencseleves krumplival.", HowToText = "A lencsét és a krumplit puhára főzzük, babérlevéllel ízesítjük.", AuthorEmail = "nono@izkalauz.hu", ImagePath = "lencseleves.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "lencse", Quantity = 250, Unit = "g" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Zöld szelet", Category = "Főétel", Description = "Különleges zöldséges-tojásos sütemény.", HowToText = "A zöldségeket a tojással összekeverjük, majd tepsiben aranybarnára sütjük.", AuthorEmail = "nono@izkalauz.hu", ImagePath = "zold_szelet.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "tojás", Quantity = 4, Unit = "db" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Túrós kocka", Category = "Főétel", Description = "Klasszikus magyar tésztaétel.", HowToText = "Kifőzzük a tésztát, összekeverjük a túróval és tejföllel.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "turos_kocka.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "túró", Quantity = 250, Unit = "g" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Rókagomba leves", Category = "Leves", Description = "Erdei gombás különlegesség.", HowToText = "A gombát hagymán pároljuk, majd tejszínnel összefőzzük.", AuthorEmail = "nono@izkalauz.hu", ImagePath = "rokagomba_leves.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "rókagomba", Quantity = 300, Unit = "g" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Fehér Kenyér", Category = "Egyéb", Description = "Ropogós házi kenyér.", HowToText = "Összegyúrjuk, kelesztjük és kisütjük.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "feher_kenyer.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "liszt", Quantity = 1, Unit = "kg" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Esküvői torta", Category = "Desszert", Description = "Ünnepi többemeletes torta.", HowToText = "Megsütjük a lapokat, megtöltjük krémmel és díszítjük.", AuthorEmail = "nono@izkalauz.hu", ImagePath = "eskuvoi_torta.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "tojás", Quantity = 12, Unit = "db" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Tormás sajttekercs", Category = "Előétel", Description = "Hidegtálak sztárja.", HowToText = "A megolvasztott sajtot tormás krémmel töltjük és feltekerjük.", AuthorEmail = "nono@izkalauz.hu", ImagePath = "tormas_sajttekercs.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "sajt", Quantity = 20, Unit = "dkg" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Szalagos fánk", Category = "Desszert", Description = "Pehelykönnyű fánk.", HowToText = "Kelesztjük, szaggatjuk és forró olajban kisütjük.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "szalagos_fank.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "liszt", Quantity = 500, Unit = "g" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Krumplis szalonna", Category = "Főétel", Description = "Egyszerű paraszti ebéd.", HowToText = "A szalonnát kipirítjuk, a krumplit vele együtt megsütjük.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "krumplis_szalonna.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "burgonya", Quantity = 1, Unit = "kg" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Karfiol leves", Category = "Leves", Description = "Zöldséges karfiolleves.", HowToText = "A zöldségeket puhára főzzük.", AuthorEmail = "nono@izkalauz.hu", ImagePath = "karfiol_leves.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "karfiol", Quantity = 1, Unit = "db" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Almás lepény", Category = "Desszert", Description = "Hagyományos almás süti.", HowToText = "Az almás tölteléket a tésztába csomagoljuk és kisütjük.", AuthorEmail = "nono@izkalauz.hu", ImagePath = "almas_lepeny.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "alma", Quantity = 1, Unit = "kg" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Csokis mézes szelet", Category = "Desszert", Description = "Puha mézes lapok csokival.", HowToText = "A lapokat megtöltjük krémmel, a tetejét csokival vonjuk be.", AuthorEmail = "anna@izkalauz.hu", ImagePath = "csokis_mezes.jpg", IsApproved = true, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "méz", Quantity = 100, Unit = "g" } } }
                };

                foreach (var r in recipes)
                {
                    r.CreatedAt = DateTime.UtcNow;
                    r.UpdatedAt = DateTime.UtcNow;
                    if (r.Ingredients != null)
                    {
                        foreach (var ing in r.Ingredients) { ing.RecipeId = r.Id; }
                    }
                }
                await context.Recipes.AddRangeAsync(recipes);
                await context.SaveChangesAsync();
            }
        }
    }
}