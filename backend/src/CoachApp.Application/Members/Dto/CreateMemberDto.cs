using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace CoachApp.Members.Dto
{
    [AutoMapTo(typeof(Entities.Member))]
    public class CreateMemberDto
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(256)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        public DateTime? BirthDate { get; set; }

        public int SessionBalance { get; set; }

        public int Status { get; set; }

        public DateTime? FrozenUntil { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }

    }
}