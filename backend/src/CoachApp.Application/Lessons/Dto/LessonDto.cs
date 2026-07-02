using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CoachApp.Lessons.Dto
{
    [AutoMapFrom(typeof(Entities.Lesson))]
    public class LessonDto : EntityDto<long>
    {
        public DateTime LessonDate { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string Title { get; set; }

        public string Notes { get; set; }

        public int Status { get; set; }

        /// <summary>
        /// String form of the status — used by flow conditions (triggerData.statusName equals "PendingX").
        /// </summary>
        public string StatusName { get; set; }

        public long CoachId { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

    }
}