using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace CoachApp.Entities
{
    [Table("SessionPackages")]
    public class SessionPackage : FullAuditedEntity<long>
    {
        [Required]
        [MaxLength(200)]
        public string PackageName { get; set; }

        public int SessionCount { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }

    }
}