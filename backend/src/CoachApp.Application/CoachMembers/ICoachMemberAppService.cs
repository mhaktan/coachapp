using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CoachApp.CoachMembers.Dto;

namespace CoachApp.CoachMembers
{
    public interface ICoachMemberAppService : IAsyncCrudAppService<
        CoachMemberDto,
        long,
        PagedCoachMemberResultRequestDto,
        CreateCoachMemberDto,
        CoachMemberDto>
    {
    }
}
