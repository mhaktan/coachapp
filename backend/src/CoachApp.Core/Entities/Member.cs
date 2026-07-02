using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace CoachApp.Entities
{
    // State Machine: status — Active → Frozen → Passive
    // Initial: Active | Transitions: Active→Frozen[Freeze], Frozen→Active[Unfreeze], Active→Passive[Deactivate], Frozen→Passive[Deactivate], Passive→Active[Reactivate]
    [Table("Members")]
    public class Member : FullAuditedEntity<long>
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(256)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        public DateTime? BirthDate { get; set; }

        public int SessionBalance { get; set; }

        public Status Status { get; set; }

        public DateTime? FrozenUntil { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }

        public virtual ICollection<CoachMember> CoachMembers { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }

    }
}