using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace CoachApp.Lessons.Dto
{
    [AutoMapTo(typeof(Entities.Lesson))]
    public class CreateLessonDto
    {
        public DateTime LessonDate { get; set; }

        [MaxLength(5)]
        public string StartTime { get; set; }

        [MaxLength(5)]
        public string EndTime { get; set; }

        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        public int Status { get; set; }

        public long CoachId { get; set; }

    }
}