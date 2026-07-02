using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace CoachApp.Attendances.Dto
{
    [AutoMapTo(typeof(Entities.Attendance))]
    public class CreateAttendanceDto
    {
        public bool Attended { get; set; }

        public bool SessionDeducted { get; set; }

        [MaxLength(300)]
        public string Notes { get; set; }

        public long LessonId { get; set; }

        public long MemberId { get; set; }

    }
}