using AutoMapper;
using CoachApp.Entities;
using CoachApp.CoachMembers.Dto;

namespace CoachApp.CoachMembers
{
    public class CoachMemberMapProfile : Profile
    {
        public CoachMemberMapProfile()
        {
            CreateMap<CoachMember, CoachMemberDto>();
            CreateMap<CreateCoachMemberDto, CoachMember>();
            CreateMap<CoachMemberDto, CoachMember>();
        }
    }
}
