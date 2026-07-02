using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CoachApp.Attendances.Dto
{
    [AutoMapFrom(typeof(Entities.Attendance))]
    public class AttendanceDto : EntityDto<long>
    {
        public bool Attended { get; set; }

        public bool SessionDeducted { get; set; }

        public string Notes { get; set; }

        public long LessonId { get; set; }

        public long MemberId { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

    }
}