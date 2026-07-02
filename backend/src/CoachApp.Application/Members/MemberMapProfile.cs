using AutoMapper;
using CoachApp.Entities;
using CoachApp.Members.Dto;

namespace CoachApp.Members
{
    public class MemberMapProfile : Profile
    {
        public MemberMapProfile()
        {
            CreateMap<Member, MemberDto>()
                .ForMember(d => d.StatusName, o => o.MapFrom(s => s.Status.ToString()));
            CreateMap<CreateMemberDto, Member>();
            CreateMap<MemberDto, Member>();
        }
    }
}
