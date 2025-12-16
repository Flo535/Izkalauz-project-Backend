namespace IzKalauzBackend.DTOs
{
    public class CreateWeeklyMenuItemDto
    {
        public int DayOfWeek { get; set; }
        public Guid? SoupId { get; set; }
        public Guid MainCourseId { get; set; }
        public Guid? DessertId { get; set; }
    }
}
