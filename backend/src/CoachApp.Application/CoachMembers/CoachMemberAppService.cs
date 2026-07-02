using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using CoachApp.Entities;
using CoachApp.CoachMembers.Dto;
using CoachApp.Authorization;

namespace CoachApp.CoachMembers
{
    public class CoachMemberAppService : AsyncCrudAppService<
        CoachMember,
        CoachMemberDto,
        long,
        PagedCoachMemberResultRequestDto,
        CreateCoachMemberDto,
        CoachMemberDto>,
        ICoachMemberAppService
    {
        public CoachMemberAppService(IRepository<CoachMember, long> repository)
            : base(repository)
        {
            // Claim-based authorization (JwtPermissionChecker reads JWT "permission" claims)
            GetPermissionName = PermissionNames.CoachMember_Read;
            GetAllPermissionName = PermissionNames.CoachMember_Read;
            CreatePermissionName = PermissionNames.CoachMember_Create;
            UpdatePermissionName = PermissionNames.CoachMember_Update;
            DeletePermissionName = PermissionNames.CoachMember_Delete;
        }

        protected override IQueryable<CoachMember> CreateFilteredQuery(PagedCoachMemberResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x =>
                    x.Id.ToString().Contains(input.Keyword))
                .WhereIf(input.AssignedAt.HasValue, x => x.AssignedAt == input.AssignedAt.Value)
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive.Value)
                .WhereIf(input.MemberId.HasValue, x => x.MemberId == input.MemberId.Value)
                .WhereIf(input.CoachId.HasValue, x => x.CoachId == input.CoachId.Value);
        }
    }
}
