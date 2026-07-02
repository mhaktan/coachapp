using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using CoachApp.Entities;
using CoachApp.Lessons.Dto;
using CoachApp.Authorization;

namespace CoachApp.Lessons
{
    public class LessonAppService : AsyncCrudAppService<
        Lesson,
        LessonDto,
        long,
        PagedLessonResultRequestDto,
        CreateLessonDto,
        LessonDto>,
        ILessonAppService
    {
        private readonly IRepository<StatusChangeLog, long> _statusChangeLogRepo;
        public LessonAppService(IRepository<Lesson, long> repository, IRepository<StatusChangeLog, long> statusChangeLogRepo)
            : base(repository)
        {
            _statusChangeLogRepo = statusChangeLogRepo;
            // Claim-based authorization (JwtPermissionChecker reads JWT "permission" claims)
            GetPermissionName = PermissionNames.Lesson_Read;
            GetAllPermissionName = PermissionNames.Lesson_Read;
            CreatePermissionName = PermissionNames.Lesson_Create;
            UpdatePermissionName = PermissionNames.Lesson_Update;
            DeletePermissionName = PermissionNames.Lesson_Delete;
        }

        protected override IQueryable<Lesson> CreateFilteredQuery(PagedLessonResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x =>
                    x.Id.ToString().Contains(input.Keyword) ||
                    (x.StartTime != null && x.StartTime.Contains(input.Keyword)) ||
                    (x.EndTime != null && x.EndTime.Contains(input.Keyword)) ||
                    (x.Title != null && x.Title.Contains(input.Keyword)) ||
                    (x.Notes != null && x.Notes.Contains(input.Keyword)))
                .WhereIf(!input.StartTime.IsNullOrWhiteSpace(), x => x.StartTime != null && x.StartTime.Contains(input.StartTime))
                .WhereIf(!input.EndTime.IsNullOrWhiteSpace(), x => x.EndTime != null && x.EndTime.Contains(input.EndTime))
                .WhereIf(!input.Title.IsNullOrWhiteSpace(), x => x.Title != null && x.Title.Contains(input.Title))
                .WhereIf(!input.Notes.IsNullOrWhiteSpace(), x => x.Notes != null && x.Notes.Contains(input.Notes))
                .WhereIf(input.LessonDate.HasValue, x => x.LessonDate == input.LessonDate.Value)
                .WhereIf(input.Status.HasValue, x => x.Status == (Status)input.Status.Value)
                .WhereIf(input.CoachId.HasValue, x => x.CoachId == input.CoachId.Value);
        }

        public override async Task<LessonDto> UpdateAsync(LessonDto input)
        {
            // State machine: validate status transition + log
            var existing = await Repository.GetAsync(input.Id);
            if ((int)existing.Status != input.Status)
            {
                var fromStatus = existing.Status.ToString();
                var toStatus = ((Status)input.Status).ToString();
                ValidateStatusTransition(existing.Status, (Status)input.Status);

                await _statusChangeLogRepo.InsertAsync(new StatusChangeLog
                {
                    EntityType = "Lesson",
                    EntityId = input.Id.ToString(),
                    FromStatus = fromStatus,
                    ToStatus = toStatus,
                    Action = "Update",
                    ChangedByUserId = AbpSession.UserId
                });
            }

            return await base.UpdateAsync(input);
        }

        [Abp.Authorization.AbpAuthorize(PermissionNames.Lesson_Update)]
        public async Task<LessonDto> ChangeStatusAsync(long id, ChangeStatusInput input)
        {
            var entity = await Repository.GetAsync(id);
            var currentStatus = entity.Status.ToString();

            // Find valid transition
            var transitions = new (string From, string To, string Action, bool Readonly)[]
            {
            ("Planned", "Completed", "Complete", false),
            ("Planned", "Cancelled", "Cancel", false)
            };

            var transition = transitions.FirstOrDefault(t =>
                (t.From == "*" || t.From == currentStatus) && t.Action == input.Action);

            if (transition == default)
                throw new Abp.UI.UserFriendlyException($"Invalid action '{input.Action}' from status '{currentStatus}'");

            // Validate required fields per transition
            // No required fields for any transition

            var fromStatus = currentStatus;

            // Apply new status
            entity.Status = (Status)Enum.Parse(typeof(Status), transition.To);
            await Repository.UpdateAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            // No approval records to cancel (approvals not enabled for this project)

            // Log status change
            await _statusChangeLogRepo.InsertAsync(new Entities.StatusChangeLog
            {
                EntityType = "Lesson",
                EntityId = id.ToString(),
                FromStatus = fromStatus,
                ToStatus = transition.To,
                Action = input.Action,
                Comment = input.ActionData != null && input.ActionData.ContainsKey("comment") ? input.ActionData["comment"] : null,
                ChangedByUserId = AbpSession.UserId
            });

            var result = MapToEntityDto(entity);


            return result;
        }

        private void ValidateStatusTransition(Status from, Status to)
        {
            var allowed = new (string From, string To)[]
            {
                ("Planned", "Completed"),
                ("Planned", "Cancelled")
            };

            var isValid = allowed.Any(t =>
                (t.From == "*" || t.From == from.ToString()) &&
                t.To == to.ToString());

            if (!isValid)
                throw new Abp.UI.UserFriendlyException($"Invalid status transition from {from} to {to}");
        }
    }
}
