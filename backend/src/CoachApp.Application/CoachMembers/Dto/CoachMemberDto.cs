using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CoachApp.CoachMembers.Dto
{
    [AutoMapFrom(typeof(Entities.CoachMember))]
    public class CoachMemberDto : EntityDto<long>
    {
        public DateTime AssignedAt { get; set; }

        public bool IsActive { get; set; }

        public long MemberId { get; set; }

        public long CoachId { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

    }
}