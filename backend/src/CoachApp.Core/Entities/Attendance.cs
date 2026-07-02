using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace CoachApp.Entities
{
    [Table("Attendances")]
    public class Attendance : FullAuditedEntity<long>
    {
        public bool Attended { get; set; }

        public bool SessionDeducted { get; set; }

        [MaxLength(300)]
        public string Notes { get; set; }

        public long LessonId { get; set; }

        [ForeignKey(nameof(LessonId))]
        public virtual Lesson Lesson { get; set; }

        public long MemberId { get; set; }

        [ForeignKey(nameof(MemberId))]
        public virtual Member Member { get; set; }

    }
}