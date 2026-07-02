using AutoMapper;
using CoachApp.Entities;
using CoachApp.Lessons.Dto;

namespace CoachApp.Lessons
{
    public class LessonMapProfile : Profile
    {
        public LessonMapProfile()
        {
            CreateMap<Lesson, LessonDto>()
                .ForMember(d => d.StatusName, o => o.MapFrom(s => s.Status.ToString()));
            CreateMap<CreateLessonDto, Lesson>();
            CreateMap<LessonDto, Lesson>();
        }
    }
}
