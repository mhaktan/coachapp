using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using CoachApp.Entities;
using CoachApp.Coachs.Dto;
using CoachApp.Authorization;

namespace CoachApp.Coachs
{
    public class CoachAppService : AsyncCrudAppService<
        Coach,
        CoachDto,
        long,
        PagedCoachResultRequestDto,
        CreateCoachDto,
        CoachDto>,
        ICoachAppService
    {
        public CoachAppService(IRepository<Coach, long> repository)
            : base(repository)
        {
            // Claim-based authorization (JwtPermissionChecker reads JWT "permission" claims)
            GetPermissionName = PermissionNames.Coach_Read;
            GetAllPermissionName = PermissionNames.Coach_Read;
            CreatePermissionName = PermissionNames.Coach_Create;
            UpdatePermissionName = PermissionNames.Coach_Update;
            DeletePermissionName = PermissionNames.Coach_Delete;
        }

        protected override IQueryable<Coach> CreateFilteredQuery(PagedCoachResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x =>
                    x.Id.ToString().Contains(input.Keyword) ||
                    (x.FirstName != null && x.FirstName.Contains(input.Keyword)) ||
                    (x.LastName != null && x.LastName.Contains(input.Keyword)) ||
                    (x.Email != null && x.Email.Contains(input.Keyword)) ||
                    (x.Phone != null && x.Phone.Contains(input.Keyword)) ||
                    (x.Specialization != null && x.Specialization.Contains(input.Keyword)))
                .WhereIf(!input.FirstName.IsNullOrWhiteSpace(), x => x.FirstName != null && x.FirstName.Contains(input.FirstName))
                .WhereIf(!input.LastName.IsNullOrWhiteSpace(), x => x.LastName != null && x.LastName.Contains(input.LastName))
                .WhereIf(!input.Email.IsNullOrWhiteSpace(), x => x.Email != null && x.Email.Contains(input.Email))
                .WhereIf(!input.Phone.IsNullOrWhiteSpace(), x => x.Phone != null && x.Phone.Contains(input.Phone))
                .WhereIf(!input.Specialization.IsNullOrWhiteSpace(), x => x.Specialization != null && x.Specialization.Contains(input.Specialization))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive.Value);
        }
    }
}
