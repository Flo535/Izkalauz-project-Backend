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

            // --- RECEPTEK ÉS HOZZÁVALÓK ---
            if (!context.Recipes.Any())
            {
                var recipes = new List<Recipe>
                {
                    new Recipe {
                        Id = Guid.NewGuid(), Title = "Csirkepaprikás", Category = "Főétel", Description = "Klasszikus magyar fogás szaftosan.", HowToText = "A hagymát pirítsuk meg, adjuk hozzá a húst és a paprikát, majd tejföllel habarjuk.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "csirkep_aprikas.jpg",
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "csirkecomb", Quantity = 1, Unit = "kg" },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tejföl", Quantity = 200, Unit = "ml" }
                        }
                    },
                    new Recipe {
                        Id = Guid.NewGuid(), Title = "Gulyásleves", Category = "Leves", Description = "Hagyományos marhagulyás.", HowToText = "A marhahúst pörkölt alapon indítjuk, majd zöldségekkel és vízzel felöntve készre főzzük.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "gulyas_leves.jpg",
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "marhahús", Quantity = 0.6m, Unit = "kg" },
                            new Ingredient { Id = Guid.NewGuid(), Name = "burgonya", Quantity = 0.5m, Unit = "kg" }
                        }
                    },
                    new Recipe {
                        Id = Guid.NewGuid(), Title = "Pestos karaj", Category = "Főétel", Description = "Olaszos ízvilágú sült hús.", HowToText = "A karajszeleteket megkenjük pestoval és tepsiben összesütjük.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "pestos_karaj.jpg",
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "sertéskaraj", Quantity = 4, Unit = "szelet" },
                            new Ingredient { Id = Guid.NewGuid(), Name = "bazsalikom pesto", Quantity = 100, Unit = "g" }
                        }
                    },
                    new Recipe {
                        Id = Guid.NewGuid(), Title = "Citromos süti", Category = "Desszert", Description = "Frissítő és puha édesség.", HowToText = "A hozzávalókat összekeverjük, citromhéjat reszelünk bele és tűpróbáig sütjük.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "pihe-puha_citromos_suti.jpg",
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "liszt", Quantity = 250, Unit = "g" },
                            new Ingredient { Id = Guid.NewGuid(), Name = "citrom", Quantity = 2, Unit = "db" }
                        }
                    },
                    new Recipe {
                        Id = Guid.NewGuid(), Title = "Rakott krumpli", Category = "Főétel", Description = "Sok kolbásszal és tojással.", HowToText = "A főtt krumplit, tojást és kolbászt rétegezzük, majd sok tejföllel leöntve kisütjük.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "rakott_krumpli.jpg",
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "krumpli", Quantity = 1, Unit = "kg" },
                            new Ingredient { Id = Guid.NewGuid(), Name = "kolbász", Quantity = 20, Unit = "dkg" }
                        }
                    },
                    new Recipe {
                        Id = Guid.NewGuid(), Title = "Túróscsusza", Category = "Főétel", Description = "Házi túróval és pörccel.", HowToText = "A tésztát kifőzzük, összekeverjük a túróval és tejföllel, a tetejére szalonnapörcöt teszünk.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "turoscsusza.jpg",
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "csusztatészta", Quantity = 500, Unit = "g" },
                            new Ingredient { Id = Guid.NewGuid(), Name = "túró", Quantity = 250, Unit = "g" }
                        }
                    },
                    new Recipe {
                        Id = Guid.NewGuid(), Title = "Paradicsomos tészta", Category = "Főétel", Description = "Gyors és egyszerű ebéd.", HowToText = "A tésztát kifőzzük, a szószt bazsalikommal összefőzzük.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "paradicsomos_teszta.jpg",
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "spagetti", Quantity = 500, Unit = "g" },
                            new Ingredient { Id = Guid.NewGuid(), Name = "paradicsomszósz", Quantity = 400, Unit = "ml" }
                        }
                    },
                    new Recipe {
                        Id = Guid.NewGuid(), Title = "Krémleves", Category = "Leves", Description = "Selymes zöldségkrémleves.", HowToText = "A zöldségeket puhára főzzük, majd botmixerrel pürésítjük.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "krem_leves.webp",
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "vegyes zöldség", Quantity = 0.5m, Unit = "kg" },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tejszín", Quantity = 200, Unit = "ml" }
                        }
                    },
                    new Recipe {
                        Id = Guid.NewGuid(), Title = "Almás piskóta", Category = "Desszert", Description = "Klasszikus nagymama-féle süti.", HowToText = "A piskótatésztába almát szeletelünk és fahéjjal megszórjuk.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "almas_piskota.jpg",
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "alma", Quantity = 3, Unit = "db" },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tojás", Quantity = 6, Unit = "db" }
                        }
                    },
                    new Recipe {
                        Id = Guid.NewGuid(), Title = "Málnás túrótorta", Category = "Desszert", Description = "Sütés nélküli finomság.", HowToText = "A kekszes alapra ráöntjük a túrókrémet és málnával díszítjük.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "malnas_turotorta.jpg",
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "túró", Quantity = 500, Unit = "g" },
                            new Ingredient { Id = Guid.NewGuid(), Name = "málna", Quantity = 200, Unit = "g" }
                        }
                    },
                    new Recipe {
                        Id = Guid.NewGuid(), Title = "Óvári sertés", Category = "Főétel", Description = "Gombával, sonkával, sajttal.", HowToText = "A húst elősütjük, rátesszük a feltéteket és ráolvasztjuk a sajtot.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "ovari_sertesszelet.jpg",
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "sertésszelet", Quantity = 4, Unit = "db" },
                            new Ingredient { Id = Guid.NewGuid(), Name = "gomba", Quantity = 200, Unit = "g" }
                        }
                    },
                    new Recipe { Id = Guid.NewGuid(), Title = "Rizses-babos hús", Category = "Főétel", Description = "Mexikói ihletésű egytálétel.", HowToText = "Mindent egy edényben készre párolunk.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "rizses-babos_hus.jpg", Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "rizs", Quantity = 200, Unit = "g" }, new Ingredient { Id = Guid.NewGuid(), Name = "bab", Quantity = 1, Unit = "konzerv" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Sertéshús pak choi-jal", Category = "Főétel", Description = "Különleges keleti ízek.", HowToText = "Wokban pirítjuk a hozzávalókat.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "serteshus_pak_choi-jal_rizzsel.jpg", Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "sertés szűz", Quantity = 400, Unit = "g" }, new Ingredient { Id = Guid.NewGuid(), Name = "pak choi", Quantity = 2, Unit = "db" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Tepsis fasírt", Category = "Főétel", Description = "Olajszag nélküli fasírt.", HowToText = "Tepsiben, sütőpapíron sütjük ki a golyókat.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "tepsiben_sult_fasirt.jpg", Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "darált hús", Quantity = 0.5m, Unit = "kg" }, new Ingredient { Id = Guid.NewGuid(), Name = "zsemle", Quantity = 1, Unit = "db" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Zöldborsóleves", Category = "Leves", Description = "Édeskés, tavaszi leves.", HowToText = "Vajon futtatjuk a borsót, majd felöntjük.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "zoldborsoleves.jpg", Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "borsó", Quantity = 400, Unit = "g" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Zöldségleves", Category = "Leves", Description = "Sok friss zöldséggel.", HowToText = "Minden zöldséget felaprítunk és puhára főzünk.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "zoldsegleves.jpg", Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "sárgarépa", Quantity = 2, Unit = "db" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Csokis keksz", Category = "Desszert", Description = "Amerikai típusú keksz.", HowToText = "A tésztába csokidarabokat keverünk és kisütjük.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "csokis_kekesz.jpg", Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "csokoládé", Quantity = 100, Unit = "g" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Csokis palacsinta", Category = "Desszert", Description = "A gyerekek kedvence.", HowToText = "Kiszütjük a tésztát, csokikrémmel töltjük.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "csokis_palacsinta.jpg", Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "liszt", Quantity = 300, Unit = "g" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Fahéjas tekercs", Category = "Desszert", Description = "Kelt tészta fahéjjal.", HowToText = "Megkelesztjük, töltjük, tekerjük és sütjük.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "fahejas_tekercs.jpg", Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "élesztő", Quantity = 25, Unit = "g" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Kecskesajtos paradicsom", Category = "Előétel", Description = "Könnyű ínyencség.", HowToText = "A paradicsomokat megtöltjük kecskesajttal.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "kecskesajtos_paradicsom.jpg", Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "kecskesajt", Quantity = 150, Unit = "g" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Menzás piskóta", Category = "Desszert", Description = "Csokiöntettel, ahogy régen.", HowToText = "Magas piskótát sütünk és híg csokiöntetet készítünk.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "menzas_csokiontetes_piskota.jpg", Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "kakaópor", Quantity = 2, Unit = "ek" } } },
                    new Recipe { Id = Guid.NewGuid(), Title = "Paradicsomos káposzta", Category = "Főétel", Description = "Édeskés menzás kedvenc.", HowToText = "A káposztát paradicsomlében puhára főzzük.", AuthorEmail = "admin@izkalauz.hu", ImagePath = "paradicsomos_kaposzta.jpg", Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid(), Name = "káposzta", Quantity = 1, Unit = "fej" } } }
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