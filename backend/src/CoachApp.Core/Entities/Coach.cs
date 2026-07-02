using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace CoachApp.Entities
{
    [Table("Coachs")]
    public class Coach : FullAuditedEntity<long>
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(256)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(200)]
        public string Specialization { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<CoachMember> CoachMembers { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }

    }
}