using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using CoachApp.Entities;
using CoachApp.SessionPackages.Dto;
using CoachApp.Authorization;

namespace CoachApp.SessionPackages
{
    public class SessionPackageAppService : AsyncCrudAppService<
        SessionPackage,
        SessionPackageDto,
        long,
        PagedSessionPackageResultRequestDto,
        CreateSessionPackageDto,
        SessionPackageDto>,
        ISessionPackageAppService
    {
        public SessionPackageAppService(IRepository<SessionPackage, long> repository)
            : base(repository)
        {
            // Claim-based authorization (JwtPermissionChecker reads JWT "permission" claims)
            GetPermissionName = PermissionNames.SessionPackage_Read;
            GetAllPermissionName = PermissionNames.SessionPackage_Read;
            CreatePermissionName = PermissionNames.SessionPackage_Create;
            UpdatePermissionName = PermissionNames.SessionPackage_Update;
            DeletePermissionName = PermissionNames.SessionPackage_Delete;
        }

        protected override IQueryable<SessionPackage> CreateFilteredQuery(PagedSessionPackageResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x =>
                    x.Id.ToString().Contains(input.Keyword) ||
                    (x.PackageName != null && x.PackageName.Contains(input.Keyword)))
                .WhereIf(!input.PackageName.IsNullOrWhiteSpace(), x => x.PackageName != null && x.PackageName.Contains(input.PackageName))
                .WhereIf(input.SessionCount.HasValue, x => x.SessionCount == input.SessionCount.Value)
                .WhereIf(input.Price.HasValue, x => x.Price == input.Price.Value)
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive.Value);
        }
    }
}
