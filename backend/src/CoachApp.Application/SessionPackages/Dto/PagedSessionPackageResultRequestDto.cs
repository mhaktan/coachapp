using System;
using Abp.Application.Services.Dto;

namespace CoachApp.SessionPackages.Dto
{
    public class PagedSessionPackageResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public string PackageName { get; set; }
        public int? SessionCount { get; set; }
        public decimal? Price { get; set; }
        public bool? IsActive { get; set; }
    }
}
