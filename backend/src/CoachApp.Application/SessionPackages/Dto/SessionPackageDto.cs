using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CoachApp.SessionPackages.Dto
{
    [AutoMapFrom(typeof(Entities.SessionPackage))]
    public class SessionPackageDto : EntityDto<long>
    {
        public string PackageName { get; set; }

        public int SessionCount { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

    }
}