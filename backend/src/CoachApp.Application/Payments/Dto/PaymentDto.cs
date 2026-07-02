using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CoachApp.Payments.Dto
{
    [AutoMapFrom(typeof(Entities.Payment))]
    public class PaymentDto : EntityDto<long>
    {
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public int PaymentMethod { get; set; }

        public int SessionsPurchased { get; set; }

        public string Notes { get; set; }

        public long MemberId { get; set; }

        public long SessionPackageId { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

    }
}