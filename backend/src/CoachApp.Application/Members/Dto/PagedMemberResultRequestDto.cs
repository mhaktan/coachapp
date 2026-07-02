using System;
using Abp.Application.Services.Dto;

namespace CoachApp.Members.Dto
{
    public class PagedMemberResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? SessionBalance { get; set; }
        public int? Status { get; set; }
        public DateTime? FrozenUntil { get; set; }
        public string Notes { get; set; }
    }
}
