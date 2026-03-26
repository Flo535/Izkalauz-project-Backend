using AutoMapper;
using IzKalauzBackend.DTOs;
using IzKalauzBackend.Models;

namespace IzKalauzBackend.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Recipe, WeeklyRecipeDto>();
            CreateMap<WeeklyMenuItem, WeeklyMenuDto>();

            // Ha fordítva is kell (pl. Create DTO → Entity)
            CreateMap<CreateWeeklyMenuItemDto, WeeklyMenuItem>()
                .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => (DayOfWeek)src.DayOfWeek));
        }
    }
}
