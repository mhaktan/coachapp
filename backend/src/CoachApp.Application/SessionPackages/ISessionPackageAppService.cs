using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CoachApp.SessionPackages.Dto;

namespace CoachApp.SessionPackages
{
    public interface ISessionPackageAppService : IAsyncCrudAppService<
        SessionPackageDto,
        long,
        PagedSessionPackageResultRequestDto,
        CreateSessionPackageDto,
        SessionPackageDto>
    {
    }
}
