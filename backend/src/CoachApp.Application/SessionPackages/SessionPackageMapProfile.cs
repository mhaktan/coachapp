using AutoMapper;
using CoachApp.Entities;
using CoachApp.SessionPackages.Dto;

namespace CoachApp.SessionPackages
{
    public class SessionPackageMapProfile : Profile
    {
        public SessionPackageMapProfile()
        {
            CreateMap<SessionPackage, SessionPackageDto>();
            CreateMap<CreateSessionPackageDto, SessionPackage>();
            CreateMap<SessionPackageDto, SessionPackage>();
        }
    }
}
