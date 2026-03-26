using System.ComponentModel.DataAnnotations;

namespace IzKalauzBackend.Models
{
    public class WeeklyMenuItem
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public DayOfWeek DayOfWeek { get; set; }    //1=Hétfő, 2=Kedd....

        public Guid? SoupId { get; set; }
        public Recipe? Soup { get; set; }           // opcionális

        public Guid MainCourseId { get; set; }
        public Recipe MainCourse { get; set; } = null!;

        public Guid? DessertId { get; set; }         // opcionális
        public Recipe? Dessert { get; set; }
    }
}
