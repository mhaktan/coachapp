using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace CoachApp.Entities
{
    // State Machine: status — Planned → Completed → Cancelled
    // Initial: Planned | Transitions: Planned→Completed[Complete], Planned→Cancelled[Cancel]
    [Table("Lessons")]
    public class Lesson : FullAuditedEntity<long>
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

        public Status Status { get; set; }

        public long CoachId { get; set; }

        [ForeignKey(nameof(CoachId))]
        public virtual Coach Coach { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }

    }
}