using System;
using Abp.Application.Services.Dto;

namespace CoachApp.Payments.Dto
{
    public class PagedPaymentResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public long? MemberId { get; set; }
        public long? SessionPackageId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int? PaymentMethod { get; set; }
        public int? SessionsPurchased { get; set; }
        public string Notes { get; set; }
    }
}
