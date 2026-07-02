using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using CoachApp.Entities;
using CoachApp.Attendances.Dto;
using CoachApp.Authorization;

namespace CoachApp.Attendances
{
    public class AttendanceAppService : AsyncCrudAppService<
        Attendance,
        AttendanceDto,
        long,
        PagedAttendanceResultRequestDto,
        CreateAttendanceDto,
        AttendanceDto>,
        IAttendanceAppService
    {
        public AttendanceAppService(IRepository<Attendance, long> repository)
            : base(repository)
        {
            // Claim-based authorization (JwtPermissionChecker reads JWT "permission" claims)
            GetPermissionName = PermissionNames.Attendance_Read;
            GetAllPermissionName = PermissionNames.Attendance_Read;
            CreatePermissionName = PermissionNames.Attendance_Create;
            UpdatePermissionName = PermissionNames.Attendance_Update;
            DeletePermissionName = PermissionNames.Attendance_Delete;
        }

        protected override IQueryable<Attendance> CreateFilteredQuery(PagedAttendanceResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x =>
                    x.Id.ToString().Contains(input.Keyword) ||
                    (x.Notes != null && x.Notes.Contains(input.Keyword)))
                .WhereIf(!input.Notes.IsNullOrWhiteSpace(), x => x.Notes != null && x.Notes.Contains(input.Notes))
                .WhereIf(input.Attended.HasValue, x => x.Attended == input.Attended.Value)
                .WhereIf(input.SessionDeducted.HasValue, x => x.SessionDeducted == input.SessionDeducted.Value)
                .WhereIf(input.LessonId.HasValue, x => x.LessonId == input.LessonId.Value)
                .WhereIf(input.MemberId.HasValue, x => x.MemberId == input.MemberId.Value);
        }
    }
}
