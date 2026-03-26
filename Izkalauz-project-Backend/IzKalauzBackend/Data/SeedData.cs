using System.Xml.Linq;
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
                    new Recipe {
                        Id = Guid.NewGuid(),
                        Title = "Csirkepaprikás",
                        Category = "Főétel",
                        Description = "Csirkepaprikás szaftos combokkal, paprikás-hagymás szafttal és tejföllel.",
                        HowToText = "A csirkecombokat (ha egész csirkét használsz, feldaraboljuk) enyhén sózzuk. A hagymát finomra vágjuk. Egy nagyobb lábasban vagy fazékban zsírt/olajat hevítünk, beletesszük a hagymát, és lassú tűzön üvegesre pároljuk (kb. 10-15 perc, ne barnuljon meg). Lehúzzuk a tűzről, hozzáadjuk a pirospaprikát (édeset, ha csípőset szeretsz, kicsit csípőset is), gyorsan elkeverjük, hogy ne égjen. Visszatesszük a tűzre, hozzáadjuk a csirkecombokat, és fehéredésig pirítjuk. Sózzuk, borsozzuk, hozzáadjuk a zúzott fokhagymát, kockázott paradicsomot és paprikát. Felöntjük annyi vízzel vagy alaplével, hogy ellepje (kb. 8-10 dl), fedő alatt lassú tűzön főzzük kb. 45-60 percig, amíg a hús omlós lesz (közben pótoljuk a folyadékot, ha kell). Ha kész, a húst kivesszük. A szaftot botmixerrel vagy villával kicsit összetörjük (ne legyen túl sima). Tejfölt elkeverünk egy kis szafttal (hogy ne csapódjon ki), majd a levesbe keverjük, felforraljuk, ízlés szerint utánasózzuk, csípős paprikával fűszerezzük. Visszatesszük a húst a szaftba. Galuskához: tojást, lisztet, sót összekeverünk, szaggatjuk a forrásban lévő szaftba vagy külön főzzük. Melegen tálaljuk friss petrezselyemmel, uborkasalátával vagy savanyúsággal.",
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "csirkep_aprikas.jpg",
                        IsApproved = true,
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "csirkecomb", Quantity = 1.2m, Unit = Unit.All[2] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vöröshagyma", Quantity = 4, Unit = Unit.All[11] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "olaj", Quantity = 4, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "fűszerpaprika", Quantity = 3, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "fokhagyma", Quantity = 3, Unit = Unit.All[12] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "paradicsom", Quantity = 2, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tv-paprika", Quantity = 2, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "bors", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tejföl", Quantity = 3, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "víz", Quantity = 1, Unit = Unit.All[5] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "csípős paprika", Quantity = 1, Unit = Unit.All[10] }
                        }
                    },
                    new Recipe {
                        Id = Guid.NewGuid(),
                        Title = "Gulyásleves", 
                        Category = "Leves",
                        Description = "Hagyományos marhagulyás sok zöldséggel, fűszeres - paprikás lében.",
                        HowToText = "A marhahúst kb. 2-3 cm-es kockákra vágjuk. A hagymát finomra aprítjuk. Egy nagyobb fazékban (vagy bográcsban) zsírt/olajat hevítünk, beletesszük a hagymát, és lassú tűzön üvegesre pároljuk (kb. 10-15 perc, ne barnuljon). Lehúzzuk a tűzről, hozzáadjuk a fűszerpaprikát (édeset és ha csípőset szeretsz, kicsit csípőset is), gyorsan elkeverjük, hogy ne égjen meg. Visszatesszük a tűzre, hozzáadjuk a húst, és fehéredésig pirítjuk. Sózzuk, borsozzuk, hozzáadjuk az őrölt köményt, zúzott fokhagymát, babérlevelet, paradicsomot és paprikát (kockázva). Felöntjük annyi vízzel/alaplével, hogy ellepje (kb. 2-2,5 l), és fedő alatt nagyon lassú tűzön főzzük kb. 1,5-2 órát, amíg a hús majdnem puha lesz (közben pótoljuk a vizet, ha kell). Közben a sárgarépát, fehérrépát és zellert karikára vágjuk, a burgonyát kockára. Ha a hús puha, hozzáadjuk a zöldségeket (először a répákat, 10 perc múlva a burgonyát), és tovább főzzük, amíg minden puha (kb. 20-30 perc). Ízlés szerint csípős paprikakrémmel vagy friss erős paprikával fűszerezzük. Ha csipetkét adunk hozzá: tojásból, lisztből, sóból kemény tésztát gyúrunk, kis darabokat csípünk bele a forrásban lévő levesbe, 5-7 percig főzzük. Végül friss petrezselyemmel megszórjuk. Melegen, friss kenyérrel tálaljuk – a szaftot is kanalazzuk mellé!",
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "gulyas_leves.jpg",
                        IsApproved = true,
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "marhalábszár", Quantity = 80, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vöröshagyma", Quantity = 5, Unit = Unit.All[11] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "olaj", Quantity = 5, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "édes fűszerpaprika", Quantity = 3, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "fokhagyma", Quantity = 4, Unit = Unit.All[12] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "bors", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "őrölt kömény", Quantity = 1, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "paradicsom", Quantity = 2, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tv-paprika", Quantity = 2, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "babérlevél", Quantity = 2, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "sárgarépa", Quantity = 30, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "fehérrépa", Quantity = 20, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "burgonya", Quantity = 60, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "víz", Quantity = 2, Unit = Unit.All[5] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "csípős paprikakrém", Quantity = 1, Unit = Unit.All[9] }
                        }
                    },
                    new Recipe {
                        Id = Guid.NewGuid(),
                        Title = "Pestós karaj",
                        Category = "Főétel",
                        Description = "Szaftos sertéskaraj pesto-val és sajttal töltve.",
                        HowToText = "A karajszeleteket enyhén kiklopfoljuk (kb. 1 cm vastagra), mindkét oldalát sózzuk, borsozzuk. Mindegyik szelet közepére kenünk 1-2 tk pestót, ráreszelünk vagy ráfektetünk reszelt/trappista sajtot (és ha van, vékony sonka szeletet). A szeleteket szorosan feltekerjük, fogpiszkálóval vagy spárgával rögzítjük (vagy bacon szalonnába göngyöljük a ropogósságért). Egy serpenyőben olívaolajon közepes tűzön mindkét oldalukat hirtelen elősütjük 2-3 percig. Átöntjük egy sütőpapírral bélelt tepsibe vagy jénai tálba, aláöntünk kevés fehérbort vagy alaplét (hogy szaftos maradjon). Előmelegített sütőben 180-190°C-on 25-35 percig sütjük (a vastagságtól függően), amíg a hús átsül (belső hőfok kb. 70-75°C). Ha baconba göngyöltük, az utolsó 5-10 percben grill funkcióval ropogósra sütjük a tetejét. Pihentetjük 5 percet, majd szeleteljük. A szaftot mártásként kínálhatjuk, vagy tejszínt keverünk bele krémes szósznak. Tálalhatjuk sült krumplival, grillezett zöldségekkel vagy rizzsel.",
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "pestos_karaj.jpg",
                        IsApproved = true,
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "sertéskaraj", Quantity = 80, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "zöld pesto", Quantity = 6, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "reszelt sajt", Quantity = 15, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "őrölt bors", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "olívaolaj", Quantity = 3, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "fehérbor", Quantity = 2, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "füstölt szalonna", Quantity = 15, Unit = Unit.All[1] }
                        }
                    },
                    new Recipe {
                        Id = Guid.NewGuid(),
                        Title = "Citromos süti",
                        Category = "Desszert",
                        Description = "Citromos süti omlós tésztával és pudingos-citromos töltelékkel.",
                        HowToText = "A tésztához a lisztet a sütőporral, sóval és porcukorral elkeverjük. Hozzáadjuk a puha vajat/margarint, a tojássárgákat, a citrom reszelt héját és annyi tejfölt, hogy rugalmas tésztát kapjunk (ne gyúrjuk túl). Két részre osztjuk (alsó nagyobb). Az alsó részt kinyújtjuk vagy tepsi aljába nyomkodjuk (sütőpapírral bélelt 25x35 cm-es tepsi), villával megszurkáljuk, előmelegített sütőben 180°C-on 10-12 percig elősütjük. Közben a krémhez a tejet a pudingporral, cukorral és tojássárgákkal sűrű pudinggá főzzük. Lehúzzuk a tűzről, belekeverjük a puha vajat és a citrom reszelt héját + levét. A langyos pudingot az elősütött tésztára kenjük. A felső tésztát reszeljük rá (vagy kinyújtjuk és rácsosra vágva tesszük rá). Visszatesszük a sütőbe 180°C-on további 20-25 percig, amíg a teteje aranybarna lesz. Hagyjuk teljesen kihűlni (jobb, ha hűtőben dermed 2-3 órát), porcukorral megszórjuk, szeleteljük. Frissen vagy hidegen is mennyei!",
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "pihe-puha_citromos_suti.jpg",
                        IsApproved = true,
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "finomliszt", Quantity = 50, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "porcukor", Quantity = 20, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "margarin", Quantity = 25, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tojássárgája", Quantity = 6, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tejföl", Quantity = 1, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "sütőpor", Quantity = 1, Unit = Unit.All[8] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "citrom", Quantity = 2, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tej", Quantity = 7, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vaníliás pudingpor", Quantity = 80, Unit = Unit.All[0] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "kristálycukor", Quantity = 15, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "puha vaj", Quantity = 20, Unit = Unit.All[1] }
                        }
                    },
                    new Recipe {
                        Id = Guid.NewGuid(),
                        Title = "Rakott krumpli",
                        Category = "Főétel",
                        Description = "Hagyományos rakott krumpli főtt burgonyával, tojással, kolbásszal és tejföllel.",
                        HowToText = "A krumplit héjában sós vízben puhára főzzük (kb. 20-30 perc, villával ellenőrizzük), majd hagyjuk kihűlni, meghámozzuk és vékony karikára vágjuk. A tojásokat keményre főzzük (10 perc), meghámozzuk és karikára vágjuk. A kolbászt vékony karikára vágjuk (vagy szeleteljük). Egy tálban a tejfölt összekeverjük egy kis sóval, borssal, opcionálisan 1-2 gerezd zúzott fokhagymával és egy csipet pirospaprikával (hogy szaftosabb legyen). Egy kivajazott vagy sütőpapírral bélelt tepsi aljába rétegezzük: krumplikarikák, sózzuk kicsit, rá kolbászkarikák, tojáskarikák, majd bőven tejfölös mártás. Ismételjük a rétegeket, amíg elfogy az alapanyag (legfelső réteg legyen tejfölös, és ha van, reszelt sajtot szórhatunk rá ropogósra). Előmelegített sütőben 180-200°C-on 40-50 percig sütjük, amíg a teteje szép aranybarna lesz és buborékol. Hagyjuk 10 percet pihenni, majd szeleteljük. Melegen tálaljuk savanyú uborkával, kovászos kenyérrel vagy salátával.",
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "rakott_krumpli.jpg",
                        IsApproved = true,
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "burgonya", Quantity = 1.5m, Unit = Unit.All[2] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tojás", Quantity = 8, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "kolbász", Quantity = 30, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tejföl", Quantity = 8, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vaj vagy zsír", Quantity = 2, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "bors", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "fokhagyma", Quantity = 2, Unit = Unit.All[12] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "őröltpaprika", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "reszelt sajt", Quantity = 15, Unit = Unit.All[1] }
                        }
                    },
                    new Recipe {
                        Id = Guid.NewGuid(),
                        Title = "Túrós csusza",
                        Category = "Főétel",
                        Description = "Egyszerű túrós csusza ropogós szalonnával, omlós túróval és bőséges tejföllel.",
                        HowToText = "A csuszatésztát (vagy fodros kocka tésztát) sós vízben a csomagolás szerint kifőzzük (kb. 8-10 perc), majd leszűrjük és hideg vízzel leöblítjük, hogy ne ragadjon össze. Közben a füstölt szalonnát (vagy húsos szalonnát) apró kockára vágjuk, száraz serpenyőben közepes tűzön ropogósra sütjük (pörcösre pirítjuk), a kisült zsírját félretesszük. A túrót villával vagy kézzel összemorzsoljuk (ne legyen túl sima, maradjon rögös). A kifőtt tésztát egy nagy tálba tesszük, ráöntjük a szalonna zsírjának felét, hozzákeverjük a tejföl felét, a túró felét, a pörcök felét és sót ízlés szerint (a túró és a szalonna is sós, óvatosan sózzunk!). Jól összeforgatjuk, hogy mindenhol egyenletesen bevonja a krémes-sós keverék. Tálaláskor a tetejére szórjuk a maradék túrót, tejfölt és pörcöt (vagy egyesével adagolva rakjuk tányérra: tészta + tejföl + túró + pörc). Melegen fogyasztjuk, mellé savanyú uborka, csalamádé vagy kovászos kenyér jár hozzá. Ha szeretnénk, egy kis tejjel hígíthatjuk krémesebbre.",
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "turoscsusza.jpg",
                        IsApproved = true,
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "csuszatészta", Quantity = 40, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "füstölt szalonna", Quantity = 25, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tehéntúró", Quantity = 50, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tejföl", Quantity = 4, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] }
                        }
                    },
                    new Recipe {
                        Id = Guid.NewGuid(),
                        Title = "Paradicsomos tészta",
                        Category = "Főétel",
                        Description = "Gyors, friss paradicsomos tészta fokhagymával és bazsalikommal.",
                        HowToText = "A spagettit sós vízben a csomagolás szerint kifőzzük (kb. 8-10 perc), majd leszűrjük, de egy kis főzővizet félreteszünk. Közben egy serpenyőben az olívaolajat felmelegítjük, beletesszük a vékonyra szeletelt vagy zúzott fokhagymát, és alacsony lángon 1-2 percig pirítjuk (ne barnuljon meg!). Hozzáadjuk a darabolt/kockázott paradicsomot (vagy passzírozott paradicsomot), sózzuk, borsozzuk, hozzáadjuk a cukrot (hogy ellensúlyozza a savasságot), és közepes lángon 10-12 percig főzzük, amíg sűrűbb szósz lesz (ha túl sűrű, a tészta főzővizével hígítjuk). Végül beletépkedjük a friss bazsalikomleveleket (vagy ha szárított, korábban adjuk hozzá). A kifőtt tésztát a szószba forgatjuk, jól átkeverjük. Tálaláskor reszelt parmezánnal vagy pecorino sajttal megszórjuk, extra bazsalikommal díszítjük. Melegen fogyasztjuk.",
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "paradicsomos_teszta.jpg",
                        IsApproved = true,
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "spagetti tészta", Quantity = 40, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "paradicsom", Quantity = 80, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "fokhagyma", Quantity = 4, Unit = Unit.All[12] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "olívaolaj", Quantity = 4, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "bazsalikom levél", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "ycukor", Quantity = 1, Unit = Unit.All[8] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "őrölt bors", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "reszelt parmezán", Quantity = 5, Unit = Unit.All[1] }
                        }
                    },
                    new Recipe {
                        Id = Guid.NewGuid(),
                        Title = "Sütőtökkrémleves",
                        Category = "Leves",
                        Description = "Édes-savanykás, krémleves.",
                        HowToText = "A hagymát dinszteljük, hozzáadjuk a felkockázott sütőtököt és krumplit, felöntjük alaplével. Puha zöldségeket botmixerrel pürésítjük, majd tejszínt, mézet és narancslevet keverünk bele. Ízesítjük fahéjjal, szerecsendióval. Tálalhatjuk tökmagolajjal és pirított tökmaggal.",
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "sutotokkremleves.jpg",
                        IsApproved = true,
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "sütőtök", Quantity = 100, Unit = Unit.All[2] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "burgonya", Quantity = 30, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vöröshagyma", Quantity = 1, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "olívaolaj", Quantity = 2, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "zöldségalaplé", Quantity = 10, Unit = Unit.All[5] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "főzőtejszín", Quantity = 2, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "méz", Quantity = 2, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "narancslé", Quantity = 1, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "fahéj", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "szerecsendió", Quantity = 1, Unit = Unit.All[10] }
                        }
                    },
                    new Recipe {
                        Id = Guid.NewGuid(),
                        Title = "Almás piskóta",
                        Category = "Desszert",
                        Description = "Puha almás piskóta fahéjas almával.",
                        HowToText = "Az almákat meghámozzuk, kimagozzuk, vékony szeletekre vagy reszeljük (ízlés szerint), összekeverjük a cukorral, fahéjjal és citromlével, hogy ne barnuljanak. A tojásokat szétválasztjuk. A sárgákat a cukorral és vaníliás cukorral habosra keverjük (kb. 5-7 perc). A fehérjéket csipet sóval kemény habbá verjük. A lisztet és sütőport összekeverjük, óvatosan a sárgás masszához forgatjuk, majd a fehérjehabot is beleforgatjuk (ne törjük össze a habot!). Egy kivajazott vagy sütőpapírral bélelt tepsi (kb. 25x35 cm) aljába egyenletesen elosztjuk az almás keveréket. Ráöntjük a piskótatésztát, óvatosan elsimítjuk. Előmelegített sütőben 170-180°C-on 35-45 percig sütjük (tűpróba: a közepébe szúrt tű tiszta maradjon). Ha kész, hagyjuk a tepsiben kicsit hűlni, majd rácsra tesszük. Porcukorral megszórjuk, kockákra vágjuk. Melegen vagy hidegen is finom, vaníliaöntettel vagy tejszínhabbal tálalható.",
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "almas_piskota.jpg",
                        IsApproved = true,
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "alma", Quantity = 1, Unit = Unit.All[2] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "cukor", Quantity = 28, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "fahéj", Quantity = 2, Unit = Unit.All[8] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "citromlé", Quantity = 1, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tojás", Quantity = 6, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vaníliás cukor", Quantity = 80, Unit = Unit.All[0] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "finomliszt", Quantity = 20, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "sütőpor", Quantity = 1, Unit = Unit.All[8] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "porcukor", Quantity = 5, Unit = Unit.All[1] }
                        }
                    },
                    new Recipe {
                        Id = Guid.NewGuid(),
                        Title = "Málnás túrótorta",
                        Category = "Desszert",
                        Description = "Málnás túrótorta kekszes alappal, túrókrémmel és málnás zselével.",
                        HowToText = "Az alaphoz a kekszet apróra daráljuk (mozsárban vagy robotgépben), összekeverjük az olvasztott vajjal. Egy kapcsos tortaforma aljába (kb. 22-24 cm) egyenletesen lenyomkodjuk, hűtőbe tesszük 15-20 percre dermedni. A krémhez a túrót villával vagy robotgéppel simára keverjük a porcukorral, vaníliás cukorral, citrom reszelt héjával és levével, majd hozzáadjuk a tejfölt. A zselatint hideg vízben áztatjuk 5 percig, majd kevés meleg vízben vagy mikróban feloldjuk (ne forrjon!). A feloldott zselatint a túrókrémbe keverjük. A habtejszínt kemény habbá verjük, óvatosan beleforgatjuk a krémbe. A krémet az alapra kenjük simára, hűtőbe tesszük 30-60 percre. Közben a málnát (friss vagy fagyasztott) a cukorral és kevés vízzel felforraljuk 5 percig, majd botmixerrel pürésítjük. A zselatint hideg vízben áztatjuk, majd a meleg málnapürébe keverjük, hogy feloldódjon. Langyosra hűtjük, majd a túrókrém tetejére óvatosan ráöntjük (hogy ne keveredjen bele). Hűtőbe tesszük legalább 4-6 órára (jobb egy éjszakára), hogy megdermedjen. Tálalás előtt friss málnával vagy citromhéjjal díszítjük, szeleteljük.",
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "malnas_turotorta.jpg",
                        IsApproved = true,
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "háztartási keksz", Quantity = 25, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vaj", Quantity = 10, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tehéntúró", Quantity = 50, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "porcukor", Quantity = 15, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vaníliás cukor", Quantity = 80, Unit = Unit.All[0] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "citrom", Quantity = 1, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tejföl", Quantity = 2, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "habtejszín", Quantity = 3, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "zselatin por", Quantity = 16, Unit = Unit.All[0] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "málna", Quantity = 40, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "cukor", Quantity = 8, Unit = Unit.All[1] }
                        }
                    },
                    new Recipe {
                        Id = Guid.NewGuid(),
                        Title = "Óvári sertés",
                        Category = "Főétel",
                        Description = "Omlós sertéskaraj sonkás-tojásos-sajtos töltelékkel, bundázva és ropogósra sütve.",
                        HowToText = "A sertéskarajt kb. 1,5-2 cm vastag szeletekre vágjuk, mindkét oldalát enyhén kiklopfoljuk, sózzuk, borsozzuk. Minden szelet közepére teszünk 1 szelet sonkát, 1 karika főtt tojást és reszelt sajtot (vagy sajt szeletet). A húst szorosan feltekerjük, fogpiszkálóval rögzítjük. A töltött tekercseket lisztbe, majd felvert tojásba, végül zsemlemorzsába forgatjuk. Serpenyőben olajon közepes tűzön mindkét oldalukat aranyszínűre sütjük (kb. 3-4 perc oldalanként). Áttesszük sütőpapírral bélelt tepsibe, előmelegített sütőben 180°C-on 20-25 percig sütjük, amíg a hús átsül (belső hőfok kb. 70-75°C). Ha ropogósabbat szeretnénk, az utolsó 5 percben grill funkcióval sütjük. Pihentetjük 5 percet, eltávolítjuk a fogpiszkálót, szeleteljük. Tálalhatjuk sült krumplival, párolt zöldséggel vagy savanyúsággal, a kisült szaftot mártásként kínálva.",
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "ovari_sertesszelet.jpg",
                        IsApproved = true,
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "sertéskaraj", Quantity = 1, Unit = Unit.All[2] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "sonka szelet", Quantity = 20, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tojás", Quantity = 9, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "reszelt sajt", Quantity = 20, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "finomliszt", Quantity = 10, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "zsemlemorzsa", Quantity = 15, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "olaj", Quantity = 1, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "bors", Quantity = 1, Unit = Unit.All[10] }
                        }
                    },
                    new Recipe {
                        Id = Guid.NewGuid(),
                        Title = "Paradicsomos egytálétel",
                        Category = "Főétel",
                        Description = "Szaftos paradicsomos tészta egytálétel sajttal a tetején.",
                        HowToText = "A tésztát (makaróni vagy csiga) sós vízben a csomagolás szerint kifőzzük (kb. 8-10 perc, de kicsit keményebbre hagyjuk, mert sütőben tovább puhul), majd leszűrjük. Közben egy serpenyőben az olajat felmelegítjük, a finomra vágott hagymát megdinszteljük üvegesre. Hozzáadjuk a zúzott fokhagymát, 1-2 percig pirítjuk, majd hozzáadjuk a paradicsompürét/sűrített paradicsomot, a darabolt paradicsomot, cukrot, sót, borsot, oregánót vagy bazsalikomot. Közepes lángon 10 percig főzzük, hogy sűrűbb szósz legyen (ha túl sűrű, kevés vizet vagy tésztafőzőlevet adunk hozzá). A kifőtt tésztát egy nagy tálba tesszük, ráöntjük a paradicsomszószt, jól összeforgatjuk. Egy kivajazott tepsi (kb. 25x35 cm) aljába kanalazzuk a tészta felét, ráreszelünk sajtot (vagy szeletet teszünk), majd a maradék tésztát, végül bőségesen sajtot a tetejére. Előmelegített sütőben 180-200°C-on 20-30 percig sütjük, amíg a teteje szép aranybarna és buborékos lesz. Hagyjuk 5-10 percet pihenni, majd szeleteljük. Melegen tálaljuk friss salátával vagy savanyúsággal.",
                        AuthorEmail = "anna@izkalauz.hu",
                        ImagePath = "paradicsomos_egytal.jpg",
                        IsApproved = true,
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "makaróni tészta", Quantity = 50, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vöröshagyma", Quantity = 1, Unit = Unit.All[11] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "fokhagyma", Quantity = 3, Unit = Unit.All[12] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "olaj", Quantity = 3, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "paradicsompüré", Quantity = 30, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "paradicsom", Quantity = 40, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "cukor", Quantity = 1, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "bors", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "bazsalikom", Quantity = 1, Unit = Unit.All[8] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "reszelt sajt", Quantity = 20, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vaj", Quantity = 1, Unit = Unit.All[9] }
                        } 
                    },
                    new Recipe {
                        Id = Guid.NewGuid(),
                        Title = "Tejszínes meggymártás",
                        Category = "Desszert",
                        Description = "Édes-savanykás meggymártás.",
                        HowToText = "A magozott meggyet egy lábasba tesszük, hozzáadjuk a cukrot, fahéjat, szegfűszeget és egy csipet sót. Lassú tűzön felforraljuk, közben a tejszínt elkeverjük a keményítővel és egy kis hideg vízzel simára keverjük. A forró meggyes léhez adjuk, folyamatosan kevergetve sűrűsítjük 2-3 percig. Ha túl sűrű, kevés vízzel hígíthatjuk. Végül ízlés szerint cukrozzuk. Melegen vagy hidegen is tálalható desszertek mellé.",
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "meggymartas.jpg",
                        IsApproved = true,
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "magozott meggy", Quantity = 50, Unit = Unit.All[2] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "cukor", Quantity = 10, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "fahéj", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "szegfűszeg", Quantity = 4, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "főzőtejszín", Quantity = 2, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "étkezési keményítő", Quantity = 3, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "víz", Quantity = 1, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] }
                        }
                    },
                    new Recipe {
                        Id = Guid.NewGuid(),
                        Title = "Epres szelet",
                        Category = "Desszert",
                        Description = "Omlós tésztás, krémes pudingos.",
                        HowToText = "A tésztához a puha vajat a porcukorral és vaníliás cukorral habosra keverjük, hozzáadjuk a tojássárgákat, majd a lisztet és sütőport. Morzsás tésztát gyúrunk, kibéleljük vele a sütőpapírral bélelt tepsit. Villával megszurkáljuk és előmelegített sütőben 180°C-on 15-18 percig sütjük. Közben a pudingot a tejjel, cukorral és vaníliás pudingporral sűrűre főzzük, langyosra hűtjük. A vajat habosra keverjük, majd a langyos pudinghoz forgatjuk. A krémet a kihűlt tésztára kenjük. Megszórjuk vagy kirakjuk a félbevágott/friss eperszemekkel. A tortazselét a cukros vízzel elkészítjük a csomagolás szerint, és óvatosan ráöntjük az eprek tetejére. Hűtőbe tesszük legalább 4-5 órára, hogy megdermedjen. Hidegen szeleteljük.",
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "epres_szelet.jpg",
                        IsApproved = true,
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "finomliszt", Quantity = 30, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "margarin", Quantity = 20, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "porcukor", Quantity = 10, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vaníliás cukor", Quantity = 2, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tojássárgája", Quantity = 3, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "sütőpor", Quantity = 1, Unit = Unit.All[7] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tej", Quantity = 7, Unit = Unit.All[5] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vaníliás pudingpor", Quantity = 2, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "kristálycukor", Quantity = 15, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vaj", Quantity = 25, Unit = Unit.All[0] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "eper", Quantity = 80, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "átlátszó tortazselé por", Quantity = 2, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "víz", Quantity = 5, Unit = Unit.All[4] }
                        }
                    },
                    new Recipe {
                        Id = Guid.NewGuid(),
                        Title = "Túrós kocka",
                        Category = "Főétel",
                        Description = "Omlós, sós túrós kocka.",
                        HowToText = "A túrót villával alaposan összetörjük egy tálban. Hozzáadjuk a lisztet, sót és a puha (vagy olvasztott) margarint/vajat. Gyors mozdulatokkal morzsás, majd egynemű tésztává dolgozzuk össze (nem kell sokáig gyúrni, csak összeálljon). Lisztezett felületen kb. 1 cm vastagra kinyújtjuk, majd éles késsel vagy derelyevágóval 2-3 cm-es kockákra vágjuk. Sütőpapírral bélelt tepsire rakosgatjuk (nem kell nagy távolság, mert nem terülnek nagyon). Felvert tojással megkenjük, szezámmaggal megszórjuk. Előmelegített sütőben 180-190°C-on sütjük 20-25 percig, amíg szép aranybarnára sülnek. Langyosan vagy hidegen is kiváló.",
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "turos_kocka.jpg",
                        IsApproved = true,
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "tehéntúró", Quantity = 25, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "finomliszt", Quantity = 25, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 2, Unit = Unit.All[7] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "margarin", Quantity = 25, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tojás", Quantity = 1, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "szezámmag", Quantity = 3, Unit = Unit.All[9] }
                        }
                    },
                    new Recipe {
                        Id = Guid.NewGuid(),
                        Title = "Házi fehér kenyér",
                        Category = "Egyéb",
                        Description = "Egyszerű házi fehér kenyér.",
                        HowToText = "Az élesztőt és a cukrot langyos vízben feloldjuk, 5-10 percig pihentetjük, amíg habosodik. Egy nagy tálba mérjük a lisztet, hozzáadjuk a sót, majd beleöntjük az élesztős vizet és az olajat. Kézzel vagy dagasztógéppel sima, rugalmas tésztát gyúrunk (kb. 8-10 percig). Letakarva, meleg helyen kelesztjük 45-60 percig, amíg duplájára nő. Lisztezett felületen átgyúrjuk, gömbölyítjük, majd enyhén lisztezett tepsire vagy jénaira tesszük. Újra kelesztjük 30-40 percig. Éles késsel bevágjuk a tetejét (3-4 mély vágás). Előmelegített sütőben 220-230°C-on 10 percig sütjük (gőzöléshez alulra tegyünk egy tepsit vízzel), majd 200°C-ra csökkentve további 25-30 percig sütjük, amíg szép aranybarna lesz és üregesen kopog az alja. Rácson hűtjük.",
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "hazi_feher_kenyer.jpg",
                        IsApproved = true,
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "finomliszt", Quantity = 67, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "langyos víz", Quantity = 4.2m, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "friss élesztő", Quantity = 25, Unit = Unit.All[0] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1.5m, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "olívaolaj vagy étolaj", Quantity = 42, Unit = Unit.All[0] },         // 42 g (kb. 4-5 ek)
                            new Ingredient { Id = Guid.NewGuid(), Name = "cukor (az élesztőnek)", Quantity = 1, Unit = Unit.All[7] }
                        }
                    },
                    new Recipe {
                        Id = Guid.NewGuid(),
                        Title = "Krumplis szalonna",
                        Category = "Főétel",
                        Description = "Egyszerű sült krumpli szalonnával és hagymával.",
                        HowToText = "A krumplit meghámozzuk és kb. 1-2 cm-es kockákra vágjuk (vagy karikára, ízlés szerint). A szalonnát kis kockákra vagy csíkokra vágjuk. Egy nagy serpenyőben (vagy tepsi alapon) a szalonnát saját zsírján közepes tűzön ropogósra pirítjuk, kiszedjük a szalonnát, de a zsírját bent hagyjuk. Ha kell, kevés olajat adunk hozzá. A hagymát apróra vágjuk, a zsíron megdinszteljük üvegesre. Hozzáadjuk a krumplit, sózzuk, borsozzuk, majoránnát (vagy paprikát) szórunk rá. Fedő alatt pároljuk 10-15 percig, majd fedő nélkül kevergetve pirítjuk, amíg a krumpli kívül ropogós, belül puha lesz. Végül visszakeverjük a pirított szalonnát, még 2-3 percig együtt pirítjuk. Tálalhatjuk savanyú uborkával, kovászos kenyérrel vagy tejföllel.",
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "krumplis_szalonna.jpg",
                        IsApproved = true,
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "burgonya", Quantity = 1, Unit = Unit.All[2] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "füstölt szalonna", Quantity = 30, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vöröshagyma", Quantity = 2, Unit = Unit.All[11] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "őrölt bors", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "majoránna", Quantity = 1, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "olaj", Quantity = 2, Unit = Unit.All[9] }
                        }
                    },
                    new Recipe {
                        Id = Guid.NewGuid(),
                        Title = "Szalagos fánk",
                        Category = "Desszert",
                        Description = "Hagyományos szalagos fánk.",
                        HowToText = "Langyos tejben feloldjuk az élesztőt, 1 kávés kanál cukrot és 2-3 evőkanál lisztet, kovászt készítünk, 10-15 percig pihentetjük, amíg habosodik. A lisztet tálba szitáljuk, közepébe mélyedést készítünk, beleöntjük a kovászt, a maradék cukrot, tojássárgákat, puha vajat, sót, reszelt citromhéjat, rumaromát és vaníliás cukrot. Lágy, rugalmas tésztát gyúrunk (kézzel 10-12 perc, géppel kevesebb), ha kell, kevés lisztet adunk hozzá. Letakarva meleg helyen duplájára kelesztjük (kb. 45-60 perc). Lisztezett felületen 1-1,5 cm vastagra kinyújtjuk, pogácsszaggatóval (vagy pohárral) kiszaggatjuk, közepébe kis mélyedést nyomunk ujjunkkal (vagy közepét kis körrel kivágjuk a szalagos forma miatt). A kiszaggatott fánkokat letakarva újabb 20-30 percig kelesztjük. Bőséges olajat forrósítunk (160-170°C), a fánkokat mindkét oldalukon aranyszínűre sütjük (oldalanként 2-3 perc, ne égjenek). Papírtörlőre szedjük lecsöpögtetni. Melegen porcukorral megszórjuk, ízlés szerint lekvárral (barack, szilva) töltve is tálalható. Frissen a legfinomabb!",
                        AuthorEmail = "admin@izkalauz.hu",
                        ImagePath = "szalagos_fank.jpg",
                        IsApproved = true,
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "finomliszt", Quantity = 50, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tej", Quantity = 3, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "élesztő", Quantity = 3, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "cukor", Quantity = 6, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tojássárgája", Quantity = 4, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vaj", Quantity = 6, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vaníliás cukor", Quantity = 1, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "citrom", Quantity = 1, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "rum", Quantity = 1, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "olaj", Quantity = 1, Unit = Unit.All[5] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "porcukor", Quantity = 5, Unit = Unit.All[1] }
                        }
                    },

                    // --- ANNA ÉS NONO RECEPTJEI ---
                    
                    new Recipe {
                        Id = Guid.NewGuid(), 
                        Title = "Klasszikus vadas", 
                        Category = "Főétel", 
                        Description = "Marhahús édes-savanykás zöldséges mártásban.", 
                        HowToText = "A marhahúst kb. 1-1,5 cm vastag szeletekre vágjuk, sózzuk, borsozzuk, kevés olajon mindkét oldalát elősütjük. Kiszedjük. Ugyanabban a lábasban / serpenyőben megdinszteljük a finomra vágott hagymát, majd hozzáadjuk a karikázott sárgarépát, fehérrépát, felkockázott zellert és petrezselyemgyökeret. Kis lángon pároljuk 10-15 percig, amíg a zöldségek kissé puhulnak. Hozzáadjuk a babérlevelet, borókabogyót, citromhéjat és levét, cukrot, majd felöntjük húslevessel és vörösborral (vagy vízzel). Visszatesszük a húst, fedő alatt nagyon lassú tűzön, vagy sütőben 160°C-on pároljuk 2-2,5 órát, amíg a hús teljesen omlós lesz. Ha kell, közben pótoljuk a folyadékot. Ha kész, kivesszük a húst, a zöldségeket botmixerrel vagy turmixgépben simára pürésítjük. A mártás sűrű, krémes legyen. Visszatesszük a húst a mártásba, hozzáadjuk a tejfölt (előtte kis mártással elkeverve, nehogy kicsapódjon), ízlés szerint sózzuk, borsozzuk, citromlével savanyítjuk. Melegen tálaljuk.", 
                        AuthorEmail = "nono@izkalauz.hu", 
                        ImagePath = "vadas.jpg", 
                        IsApproved = true,
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "marhahús", Quantity = 100, Unit = Unit.All[2] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "sárgarépa", Quantity = 50, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "fehérrépa", Quantity = 30, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "zeller", Quantity = 20, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vöröshagyma", Quantity = 2, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "olaj", Quantity = 4, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "húsleves", Quantity = 10, Unit = Unit.All[5] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vörösbor", Quantity = 3, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "cukor", Quantity = 6, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "citrom", Quantity = 1, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "babérlevél", Quantity = 3, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "borókabogyó", Quantity = 8, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "bors", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tejföl", Quantity = 3, Unit = Unit.All[4] }
                        }
                    },
                    new Recipe {
                        Id = Guid.NewGuid(), 
                        Title = "Céklakrémleves", 
                        Category = "Leves", 
                        Description = "Gyors, krémes céklakrémleves almával.", 
                        HowToText = "A céklát és az almát meghámozzuk, majd kisebb kockákra vágjuk. A hagymát finomra aprítjuk. Egy lábasban olívaolajon vagy vajon megdinszteljük a hagymát üvegesre. Hozzáadjuk a céklát és az almát, rövid ideig együtt pároljuk 3-4 percig. Felöntjük a zöldségalaplével (vagy vízzel + leveskockával), sózzuk, borsozzuk, majoránnát vagy szerecsendiót adunk hozzá ízlés szerint. Fedő alatt közepes lángon főzzük, amíg a cékla teljesen puha nem lesz (kb. 25-35 perc). Ha kész, botmixerrel simára pürésítjük. Végül hozzáadjuk a tejfölt (előtte egy kis meleg levesbe keverve, hogy ne csapódjon ki), és ízlés szerint citromlével vagy ecettel savanyítjuk. Melegen tálaljuk, tejföllel, snidlinggel vagy petrezselyemmel díszítve.", 
                        AuthorEmail = "anna@izkalauz.hu", 
                        ImagePath = "ceklakremleves.jpg", 
                        IsApproved = true, 
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "cékla", Quantity = 80, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "savanykás alma", Quantity = 20, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vöröshagyma", Quantity = 1, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "olívaolaj", Quantity = 2, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "zöldségalaplé", Quantity = 10, Unit = Unit.All[5] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tejföl", Quantity = 2, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "bors", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "citromlé", Quantity = 2, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "majoránna", Quantity = 1, Unit = Unit.All[10] }
                        } 
                    },
                    new Recipe {
                        Id = Guid.NewGuid(), 
                        Title = "Hagymakrémleves", 
                        Category = "Leves", 
                        Description = "Krémes hagymakrémleves pirított kenyérkockával.", 
                        HowToText = "A hagymát vékony karikákra vagy félkarikára vágjuk. Egy nagyobb lábasban olívaolajon vagy vajon közepes tűzön megdinszteljük a hagymát, amíg üveges és kissé megpuhul (kb. 10-12 perc, ne barnuljon nagyon). Sózzuk kicsit, hogy hamarabb levet engedjen. Hozzáadjuk a zúzott fokhagymát, és még 1-2 percig pirítjuk. Felöntjük a zöldségalaplével (vagy vízzel + leveskockával), hozzáadjuk a babérlevelet, borsot, majoránnát. Fedő alatt főzzük, amíg a hagyma teljesen puha lesz (kb. 20-25 perc). Ha kész, kivesszük a babérlevelet, botmixerrel simára pürésítjük. Visszatesszük a tűzre, hozzáadjuk a tejszínt vagy tejfölt (előtte kis meleg levesbe keverve, hogy ne csapódjon ki), ízlés szerint citromlével vagy ecettel savanyítjuk, és ha kell, utánasózzuk. Melegen tálaljuk pirított kenyérkockával, snidlinggel vagy petrezselyemmel díszítve. Ha szeretnéd franciásabbá tenni, reszelt sajtot is szórhatsz a tetejére.", 
                        AuthorEmail = "nono@izkalauz.hu", 
                        ImagePath = "hagymakremleves.jpg", 
                        IsApproved = true, 
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "vöröshagyma", Quantity = 1, Unit = Unit.All[2] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "fokhagyma", Quantity = 3, Unit = Unit.All[12] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "olívaolaj", Quantity = 4, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "zöldségalaplé", Quantity = 12, Unit = Unit.All[5] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tejföl", Quantity = 2, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "babérlevél", Quantity = 2, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "őrölt bors", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "majoránna", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "citromlé", Quantity = 2, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "pirított kenyérkocka", Quantity = 10, Unit = Unit.All[1] }
                        } 
                    },
                    new Recipe {
                        Id = Guid.NewGuid(), 
                        Title = "Lencse leves", 
                        Category = "Leves", 
                        Description = "Hagyományos magyar lencseleves zöldségekkel.", 
                        HowToText = "A lencsét átválogatjuk, megmossuk (ha van idő, 1-2 órára beáztatjuk). A zöldségeket (sárgarépa, fehérrépa, burgonya) meghámozzuk és kockára vágjuk. A hagymát finomra aprítjuk. Egy nagyobb lábasban olajon vagy zsíron megdinszteljük a hagymát, hozzáadjuk a zúzott fokhagymát, majd a lencsét és a zöldségeket. Felöntjük vízzel vagy alaplével, hozzáadjuk a babérlevelet, sót, borsot, köményt. Fedő alatt főzzük, amíg a lencse és zöldségek puhák lesznek (kb. 40-60 perc, áztatott lencsénél kevesebb). Közben rántást készítünk: kis olajon pirítjuk a lisztet, hozzáadjuk a pirospaprikát (ne égjen meg!), majd kevés vízzel felengedjük, simára keverjük és a levesbe öntjük – ezzel besűrítjük. Ízlés szerint ecettel vagy citromlével savanyítjuk, cukorral finomítjuk az ízt. Ha füstölt húst adsz hozzá, azt előre főzd puhulásig, majd kockázd bele a levesbe. Melegen tálaljuk tejföllel, friss kenyérrel. Ha sűrűbbet szeretnél, turmixolj bele egy adag levest.", 
                        AuthorEmail = "nono@izkalauz.hu", 
                        ImagePath = "lencseleves.jpg", 
                        IsApproved = true, 
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "lencse", Quantity = 30, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "sárgarépa", Quantity = 30, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "fehérrépa", Quantity = 20, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "burgonya", Quantity = 30, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vöröshagyma", Quantity = 1, Unit = Unit.All[11] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "fokhagyma", Quantity = 3, Unit = Unit.All[12] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "olaj", Quantity = 4, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "finomliszt", Quantity = 4, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "édes pirospaprika", Quantity = 2, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "víz", Quantity = 15, Unit = Unit.All[5] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "babérlevél", Quantity = 2, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "köménymag", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "bors", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "ecet", Quantity = 2, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "cukor", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tejföl", Quantity = 2, Unit = Unit.All[4] }
                        } 
                    },
                    new Recipe {
                        Id = Guid.NewGuid(), 
                        Title = "Zöld szelet", 
                        Category = "Főétel", 
                        Description = "Spenótos-tojásos zöldséges szelet sajttal.", 
                        HowToText = "A spenótot frisset megfőzzük és lecsepegtetjük. A hagymát finomra vágjuk, olajon megdinszteljük. A spenótot egy nagy tálba tesszük, hozzáadjuk a dinsztelt hagymát, zúzott fokhagymát, a felvert tojásokat, a reszelt sajt felét, tejfölt, zsemlemorzsát (vagy lisztet), sót, borsot, szerecsendiót és majoránnát. Jól összekeverjük, hogy egynemű masszát kapjunk (ha túl híg, még egy kis zsemlemorzsát adhatunk hozzá). Egy sütőpapírral bélelt tepsibe kenjük egyenletesen (kb. 3-4 cm vastagra), a tetejére megszórjuk a maradék reszelt sajtot. Előmelegített sütőben 180-190°C-on 40-50 percig sütjük, amíg a teteje szép aranybarna lesz és a közepe átsül (tűpróba). Hagyjuk kicsit hűlni, majd szeleteljük. Melegen vagy langyosan tálaljuk, tejföllel meglocsolva vagy savanyú uborkával mellé.\"", 
                        AuthorEmail = "nono@izkalauz.hu", 
                        ImagePath = "zold_szelet.jpg", 
                        IsApproved = true, 
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "spenót", Quantity = 80, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tojás", Quantity = 5, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "sajt", Quantity = 20, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tejföl", Quantity = 2, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vöröshagyma", Quantity = 1, Unit = Unit.All[11] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "fokhagyma", Quantity = 3, Unit = Unit.All[12] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "olívaolaj", Quantity = 3, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "zsemlemorzsa", Quantity = 10, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "bors", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "szerecsendió", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "majoránna", Quantity = 1, Unit = Unit.All[10] }
                        } 
                    },
                    new Recipe {
                        Id = Guid.NewGuid(),
                        Title = "Rókagomba leves",
                        Category = "Leves",
                        Description = "Aromás rókagomba leves zöldségekkel és tejföllel.",
                        HowToText = "A rókagombát megtisztítjuk majd nagyobb darabokra vágjuk. Kevés sós vízben előfőzzük 10-15 percig, majd leszűrjük. A hagymát finomra vágjuk, olajon vagy zsíron megdinszteljük üvegesre. Hozzáadjuk a zúzott fokhagymát, majd a kockázott sárgarépát, petrezselyemgyökeret és burgonyát. Rövid ideig pároljuk együtt. Felöntjük vízzel vagy alaplével, hozzáadjuk a babérlevelet, sót, borsot, majoránnát vagy tárkonyt. Beletesszük az előfőzött rókagombát, és fedő alatt főzzük, amíg a zöldségek puhák lesznek (kb. 20-30 perc). Közben rántást készítünk: kis olajon pirítjuk a lisztet, hozzáadjuk a pirospaprikát, majd kevés vízzel felengedjük és a levesbe öntjük, besűrítjük. Végül a tejfölt kis meleg levesbe keverve hozzáadjuk, citromlével vagy ecettel ízesítjük savanykásra, és ha kell, utánasózzuk. Melegen tálaljuk, snidlinggel vagy petrezselyemmel megszórva.", 
                        AuthorEmail = "nono@izkalauz.hu",
                        ImagePath = "rokagomba_leves.jpg",
                        IsApproved = true, 
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "rókagomba", Quantity = 50, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "sárgarépa", Quantity = 30, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "fehérrépa", Quantity = 20, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "burgonya", Quantity = 30, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vöröshagyma", Quantity = 1, Unit = Unit.All[11] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "fokhagyma", Quantity = 3, Unit = Unit.All[12] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "olaj", Quantity = 4, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "finomliszt", Quantity = 4, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "édes pirospaprika", Quantity = 2, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "víz", Quantity = 15, Unit = Unit.All[5] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "babérlevél", Quantity = 2, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "majoránna", Quantity = 1, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "bors", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tejföl", Quantity = 2, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "citromlé", Quantity = 2, Unit = Unit.All[9] }
                        } 
                    },
                    new Recipe {
                        Id = Guid.NewGuid(), 
                        Title = "Esküvői torta", 
                        Category = "Desszert", 
                        Description = "Egyszerű esküvői torta vaníliás piskótával és krémes mascarpone töltelékkel.", 
                        HowToText = "A piskótához a tojásokat szétválasztjuk. A sárgákat a cukorral és vaníliás cukorral habosra keverjük. A fehérjéket kemény habbá verjük. A lisztet és sütőport összekeverjük, óvatosan a sárgás masszához forgatjuk, majd a fehérjehabot is beleforgatjuk. Kivajazott, lisztezett (vagy sütőpapírral bélelt) 22 cm-es tortaformába öntjük, előmelegített sütőben 170-180°C-on sütjük 35-45 percig. Ha kész, rácson hűtjük, majd 3 lapra vágjuk. A krémhez a mascarponét a porcukorral és vaníliával elkeverjük, majd óvatosan beleforgatjuk a kemény habbá vert tejszínt. A lapokat megkenjük a krémmel (közé és tetejére is), tortává rakjuk. Külső burkoláshoz a puha vajat porcukorral habosítjuk, hozzáadjuk a tejszínt és vaníliát, sima krémet készítünk. Vékonyan rákentjük a tortára (simítóval elegáns felületet kapunk). Hűtőbe tesszük legalább 4-6 órára (jobb, ha egy éjszakára). Tálalás előtt friss virágokkal, bogyós gyümölcsökkel vagy porcukorral díszítjük. Ha emeleteset akarsz, készíts dupla adagot és tortatartó rúddal rögzítsd a szinteket.", 
                        AuthorEmail = "nono@izkalauz.hu", 
                        ImagePath = "eskuvoi_torta.jpg", 
                        IsApproved = true, 
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "tojás", Quantity = 8, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "kristálycukor", Quantity = 25, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "finomliszt", Quantity = 20, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "sütőpor", Quantity = 1, Unit = Unit.All[8] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vaníliás cukor", Quantity = 16, Unit = Unit.All[0] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "mascarpone", Quantity = 50, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "habtejszín", Quantity = 8, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "porcukor", Quantity = 15, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "puha vaj", Quantity = 30, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vanília rúd", Quantity = 2, Unit = Unit.All[6] }
                        } 
                    },
                    new Recipe {
                        Id = Guid.NewGuid(), 
                        Title = "Tormás sajttekercs", 
                        Category = "Egyéb", 
                        Description = "Olvadt sajtlapok tormás-krémes töltelékkel tekercsben.",
                        HowToText = "Először a tölteléket készítjük: egy kisebb lábasban a vajat felolvasztjuk, hozzáadjuk a lisztet, habzásig pirítjuk (1-2 perc), majd fokozatosan hozzáöntjük a tejet, folyamatosan kevergetve sűrű besamelt főzünk (kb. 5 perc). Lehúzzuk a tűzről, belekeverjük a tömlős sajtot (vagy reszelt sajtot), a reszelt tormát (ízlés szerint többet is, ha erőset szeretsz), sót, borsot. Hagyjuk langyosra hűlni. Egy sütőpapírral bélelt tepsibe (kb. 25x35 cm) egymás mellé tesszük a sajtszeleteket úgy, hogy kb. 1 cm átfedéssel fedjék egymást, egybefüggő 'lappá' alakuljanak. Előmelegített sütőben 180°C-on 5-8 percig sütjük, amíg a sajt megolvad és összeforr (ne barnuljon!). Kivesszük, a tepsiben hagyjuk kicsit hűlni (de még meleg/plasztikus állapotban legyen). A langyos tölteléket egyenletesen rásimítjuk a sajtlapra (kb. 0,5-1 cm vastagon), majd a hosszabbik oldalon szorosan feltekerjük (segítségül sütőpapírt használhatsz). Fóliába csomagoljuk, hűtőbe tesszük legalább 4-6 órára (jobb, ha egy éjszakára), hogy megdermedjen. Hidegen vékony szeletekre vágjuk, fogpiszkálóval tűzdelve tálaljuk. Díszíthető snidlinggel, petrezselyemmel vagy koktélparadicsommal.",
                        AuthorEmail = "nono@izkalauz.hu", 
                        ImagePath = "tormas_sajttekercs.jpg", 
                        IsApproved = true, 
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "sajt", Quantity = 40, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "margarin", Quantity = 5, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "finomliszt", Quantity = 3, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tej", Quantity = 3, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "reszelt sajt", Quantity = 10, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "ecetes torma", Quantity = 3, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "bors", Quantity = 1, Unit = Unit.All[10] }
                        } 
                    },
                    new Recipe {
                        Id = Guid.NewGuid(), 
                        Title = "Karfiol leves", 
                        Category = "Leves", 
                        Description = "Zöldséges karfiolleves.", 
                        HowToText = "A zöldségeket puhára főzzük.", 
                        AuthorEmail = "nono@izkalauz.hu", 
                        ImagePath = "karfiol_leves.jpg", 
                        IsApproved = true, 
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "karfiol", Quantity = 1, Unit = Unit.All[11] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "sárgarépa", Quantity = 3, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "fehérrépa", Quantity = 3, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "burgonya", Quantity = 30, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vöröshagyma", Quantity = 1, Unit = Unit.All[11] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "fokhagyma", Quantity = 2, Unit = Unit.All[12] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "olaj", Quantity = 4, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "finomliszt", Quantity = 4, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "őrölt pirospaprika", Quantity = 2, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "víz", Quantity = 15, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "babérlevél", Quantity = 2, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "bors", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tejföl", Quantity = 2, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "citromlé", Quantity = 2, Unit = Unit.All[9] }
                        } 
                    },
                    new Recipe {
                        Id = Guid.NewGuid(),
                        Title = "Almás lepény", 
                        Category = "Desszert", 
                        Description = "Omlós tésztás, reszelt almás lepény fahéjjal és porcukorral.", 
                        HowToText = "A tésztához a lisztet a sütőporral elkeverjük, hozzáadjuk a puha (vagy darabos) vajat/margarint, porcukrot, vaníliás cukrot, tojássárgákat, sót és a tejfölt. Gyorsan morzsás, majd egynemű tésztává dolgozzuk (ne gyúrjuk túl). Két részre osztjuk (egy kicsit nagyobb alsó rétegnek). Az alsó részt lisztezett felületen kinyújtjuk (vagy közvetlenül a tepsi aljába nyomkodjuk), sütőpapírral bélelt tepsi (kb. 25x35 cm) aljába tesszük, villával megszurkáljuk. A töltelékhez az almákat meghámozzuk, reszeljük (vagy apróra vágjuk), kinyomkodjuk a levét, hozzáadjuk a cukrot, fahéjat, mazsolát (ha használunk) és zsemlemorzsát (hogy ne legyen túl nedves). Egyenletesen elosztjuk a tésztán. A felső tésztát reszeljük rá (vagy kinyújtjuk rácsosra vágva). Előmelegített sütőben 180°C-on 35-45 percig sütjük, amíg a teteje szép aranybarna lesz. Hagyjuk kihűlni, porcukorral megszórjuk, kockákra vágjuk. Melegen vagy hidegen is finom!", 
                        AuthorEmail = "nono@izkalauz.hu", 
                        ImagePath = "almas_lepeny.jpg", 
                        IsApproved = true, 
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "finomliszt", Quantity = 50, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "puha vaj", Quantity = 25, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "porcukor", Quantity = 20, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vaníliás cukor", Quantity = 2, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tojássárgája", Quantity = 2, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "sütőpor", Quantity = 1, Unit = Unit.All[8] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tejföl", Quantity = 4, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "alma", Quantity = 1.5m, Unit = Unit.All[2] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "kristálycukor", Quantity = 10, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "fahéj", Quantity = 2, Unit = Unit.All[8] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "mazsola (opcionális)", Quantity = 10, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "zsemlemorzsa", Quantity = 5, Unit = Unit.All[1] }
                        } 
                    },
                    new Recipe {
                        Id = Guid.NewGuid(), 
                        Title = "Csokis mézes szelet", 
                        Category = "Desszert", 
                        Description = "Puha mézes tészta lapok csokis krémmel.", 
                        HowToText = "A tésztához a tojásokat a porcukorral, mézzel, margarinnal/vajjal, tejföllel és kakaóval egy lábasban gőz felett (vagy alacsony lángon) habosra keverjük kb. 5-7 percig, amíg langyosra hűl. Hozzáadjuk a lisztet, szódabikarbónát és sót, sima tésztát gyúrunk (ha túl ragadós, kevés lisztet adhatunk hozzá). Négy egyenlő részre osztjuk. Egy sütőpapírral bélelt tepsi hátulján (vagy sütőpapíron) vékonyra (kb. 3-4 mm) kinyújtjuk egy-egy lapot, előmelegített sütőben 180°C-on 8-10 percig sütjük (ne száradjon ki!). Még melegen vágjuk egyenesre, ha kell. A krémhez a tejet a cukorral, tojássárgákkal és liszttel sűrű pudinggá főzzük. Lehúzzuk a tűzről, belekeverjük az étcsokoládét, amíg elolvad, majd a puha vajat habosra keverjük bele. Hagyjuk langyosra hűlni. A kihűlt lapokat krémmel megkenjük (három lapra kenünk, a negyediket a tetejére tesszük). A mázhoz a csokit és a vajat gőz felett vagy mikróban felolvasztjuk, simára keverjük, és a tetejére kenjük. Hűtőbe tesszük legalább 4-6 órára (jobb egy éjszakára), hogy megdermedjen. Hidegen szeleteljük kockákra vagy téglalapokra. Porcukorral vagy reszelt csokival díszíthető.", 
                        AuthorEmail = "anna@izkalauz.hu", 
                        ImagePath = "csokis_mezes.jpg", 
                        IsApproved = true, 
                        Ingredients = new List<Ingredient> {
                            new Ingredient { Id = Guid.NewGuid(), Name = "tojás", Quantity = 3, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "porcukor", Quantity = 15, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "méz", Quantity = 3, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "margarin", Quantity = 7, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tejföl", Quantity = 1, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "kakaópor", Quantity = 3, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "finomliszt", Quantity = 60, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "szódabikarbóna", Quantity = 1, Unit = Unit.All[8] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "só", Quantity = 1, Unit = Unit.All[10] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tej", Quantity = 5, Unit = Unit.All[4] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "kristálycukor", Quantity = 20, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "tojássárgája", Quantity = 3, Unit = Unit.All[6] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "finom- vagy rétesliszt", Quantity = 3, Unit = Unit.All[9] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "vaj (krémhez)", Quantity = 25, Unit = Unit.All[1] },
                            new Ingredient { Id = Guid.NewGuid(), Name = "étcsokoládé", Quantity = 25, Unit = Unit.All[1] }
                        } 
                    }
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