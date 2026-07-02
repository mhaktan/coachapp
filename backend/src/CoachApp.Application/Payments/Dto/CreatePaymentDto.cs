using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace CoachApp.Payments.Dto
{
    [AutoMapTo(typeof(Entities.Payment))]
    public class CreatePaymentDto
    {
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public int PaymentMethod { get; set; }

        public int SessionsPurchased { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        public long MemberId { get; set; }

        public long SessionPackageId { get; set; }

    }
}