using System;
using Abp.Application.Services.Dto;

namespace CoachApp.Attendances.Dto
{
    public class PagedAttendanceResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public long? LessonId { get; set; }
        public long? MemberId { get; set; }
        public bool? Attended { get; set; }
        public bool? SessionDeducted { get; set; }
        public string Notes { get; set; }
    }
}
