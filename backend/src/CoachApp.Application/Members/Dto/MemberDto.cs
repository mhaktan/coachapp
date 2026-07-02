using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CoachApp.Members.Dto
{
    [AutoMapFrom(typeof(Entities.Member))]
    public class MemberDto : EntityDto<long>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime? BirthDate { get; set; }

        public int SessionBalance { get; set; }

        public int Status { get; set; }

        public DateTime? FrozenUntil { get; set; }

        public string Notes { get; set; }

        /// <summary>
        /// String form of the status — used by flow conditions (triggerData.statusName equals "PendingX").
        /// </summary>
        public string StatusName { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

    }
}