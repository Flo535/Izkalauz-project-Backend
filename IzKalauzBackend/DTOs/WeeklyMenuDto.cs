namespace IzKalauzBackend.DTOs
{
    public class WeeklyMenuDto
    {
        public Guid Id { get; set; }
        public int DayOfWeek { get; set; } // 0-6
        public string DayName => new[] {"Hétfő", "Kedd", "Szerda", "Csütörtök", "Péntek", "Szombat", "Vasárnap"}[DayOfWeek];
        public WeeklyRecipeDto? Soup { get; set; }
        public WeeklyRecipeDto MainCourse { get; set; } = null!;
        public WeeklyRecipeDto? Dessert { get; set; }
    }
}
