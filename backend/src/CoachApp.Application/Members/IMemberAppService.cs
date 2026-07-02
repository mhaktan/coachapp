using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CoachApp.Members.Dto;

namespace CoachApp.Members
{
    public interface IMemberAppService : IAsyncCrudAppService<
        MemberDto,
        long,
        PagedMemberResultRequestDto,
        CreateMemberDto,
        MemberDto>
    {
        Task<MemberDto> ChangeStatusAsync(long id, ChangeStatusInput input);
    }
}
