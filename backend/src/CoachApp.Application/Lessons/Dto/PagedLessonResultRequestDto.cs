using System;
using Abp.Application.Services.Dto;

namespace CoachApp.Lessons.Dto
{
    public class PagedLessonResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public long? CoachId { get; set; }
        public DateTime? LessonDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public int? Status { get; set; }
    }
}
