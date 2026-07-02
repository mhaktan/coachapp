using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace CoachApp.Entities
{
    [Table("CoachMembers")]
    public class CoachMember : FullAuditedEntity<long>
    {
        public DateTime AssignedAt { get; set; }

        public bool IsActive { get; set; }

        public long MemberId { get; set; }

        [ForeignKey(nameof(MemberId))]
        public virtual Member Member { get; set; }

        public long CoachId { get; set; }

        [ForeignKey(nameof(CoachId))]
        public virtual Coach Coach { get; set; }

    }
}