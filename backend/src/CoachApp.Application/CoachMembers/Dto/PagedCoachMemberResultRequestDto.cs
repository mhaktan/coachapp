using System;
using Abp.Application.Services.Dto;

namespace CoachApp.CoachMembers.Dto
{
    public class PagedCoachMemberResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public long? MemberId { get; set; }
        public long? CoachId { get; set; }
        public DateTime? AssignedAt { get; set; }
        public bool? IsActive { get; set; }
    }
}
