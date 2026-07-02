using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace CoachApp.CoachMembers.Dto
{
    [AutoMapTo(typeof(Entities.CoachMember))]
    public class CreateCoachMemberDto
    {
        public DateTime AssignedAt { get; set; }

        public bool IsActive { get; set; }

        public long MemberId { get; set; }

        public long CoachId { get; set; }

    }
}