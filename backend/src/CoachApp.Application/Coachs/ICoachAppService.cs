using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CoachApp.Coachs.Dto;

namespace CoachApp.Coachs
{
    public interface ICoachAppService : IAsyncCrudAppService<
        CoachDto,
        long,
        PagedCoachResultRequestDto,
        CreateCoachDto,
        CoachDto>
    {
    }
}
