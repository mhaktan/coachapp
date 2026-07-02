using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace CoachApp.SessionPackages.Dto
{
    [AutoMapTo(typeof(Entities.SessionPackage))]
    public class CreateSessionPackageDto
    {
        [Required]
        [MaxLength(200)]
        public string PackageName { get; set; }

        public int SessionCount { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

    }
}