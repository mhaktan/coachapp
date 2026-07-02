using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace CoachApp.Entities
{
    [Table("Payments")]
    public class Payment : FullAuditedEntity<long>
    {
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public int SessionsPurchased { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        public long MemberId { get; set; }

        [ForeignKey(nameof(MemberId))]
        public virtual Member Member { get; set; }

        public long SessionPackageId { get; set; }

        [ForeignKey(nameof(SessionPackageId))]
        public virtual SessionPackage SessionPackage { get; set; }

    }
}