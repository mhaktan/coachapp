using AutoMapper;
using CoachApp.Entities;
using CoachApp.Coachs.Dto;

namespace CoachApp.Coachs
{
    public class CoachMapProfile : Profile
    {
        public CoachMapProfile()
        {
            CreateMap<Coach, CoachDto>();
            CreateMap<CreateCoachDto, Coach>();
            CreateMap<CoachDto, Coach>();
        }
    }
}
