using IzKalauzBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace IzKalauzBackend.Data
{
    public static class SeedWeeklyMenu
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.WeeklyMenuItems.Any())
                return; // Már van adat, ne írjuk felül

            // Példareceptek feltételezve, hogy már léteznek a Recipes táblában
            var soups = context.Recipes.Where(r => r.Category == "Leves").Take(7).ToList();
            var mains = context.Recipes.Where(r => r.Category == "Főétel").Take(7).ToList();
            var desserts = context.Recipes.Where(r => r.Category == "Desszert").Take(7).ToList();

            for (int i = 0; i < 7; i++)
            {
                var item = new WeeklyMenuItem
                {
                    DayOfWeek = (DayOfWeek)((i + 1) % 7), // 1=Hétfő, ... 0=Vasárnap
                    SoupId = soups.ElementAtOrDefault(i)?.Id,
                    MainCourseId = mains.ElementAtOrDefault(i)?.Id ?? mains[0].Id,
                    DessertId = desserts.ElementAtOrDefault(i)?.Id
                };

                context.WeeklyMenuItems.Add(item);
            }

            context.SaveChanges();
        }
    }
}
