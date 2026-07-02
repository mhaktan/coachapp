using AutoMapper;
using CoachApp.Entities;
using CoachApp.Attendances.Dto;

namespace CoachApp.Attendances
{
    public class AttendanceMapProfile : Profile
    {
        public AttendanceMapProfile()
        {
            CreateMap<Attendance, AttendanceDto>();
            CreateMap<CreateAttendanceDto, Attendance>();
            CreateMap<AttendanceDto, Attendance>();
        }
    }
}
