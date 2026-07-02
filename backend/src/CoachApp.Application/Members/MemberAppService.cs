using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using CoachApp.Entities;
using CoachApp.Members.Dto;
using CoachApp.Authorization;

namespace CoachApp.Members
{
    public class MemberAppService : AsyncCrudAppService<
        Member,
        MemberDto,
        long,
        PagedMemberResultRequestDto,
        CreateMemberDto,
        MemberDto>,
        IMemberAppService
    {
        private readonly IRepository<StatusChangeLog, long> _statusChangeLogRepo;
        public MemberAppService(IRepository<Member, long> repository, IRepository<StatusChangeLog, long> statusChangeLogRepo)
            : base(repository)
        {
            _statusChangeLogRepo = statusChangeLogRepo;
            // Claim-based authorization (JwtPermissionChecker reads JWT "permission" claims)
            GetPermissionName = PermissionNames.Member_Read;
            GetAllPermissionName = PermissionNames.Member_Read;
            CreatePermissionName = PermissionNames.Member_Create;
            UpdatePermissionName = PermissionNames.Member_Update;
            DeletePermissionName = PermissionNames.Member_Delete;
        }

        protected override IQueryable<Member> CreateFilteredQuery(PagedMemberResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x =>
                    x.Id.ToString().Contains(input.Keyword) ||
                    (x.FirstName != null && x.FirstName.Contains(input.Keyword)) ||
                    (x.LastName != null && x.LastName.Contains(input.Keyword)) ||
                    (x.Email != null && x.Email.Contains(input.Keyword)) ||
                    (x.Phone != null && x.Phone.Contains(input.Keyword)) ||
                    (x.Notes != null && x.Notes.Contains(input.Keyword)))
                .WhereIf(!input.FirstName.IsNullOrWhiteSpace(), x => x.FirstName != null && x.FirstName.Contains(input.FirstName))
                .WhereIf(!input.LastName.IsNullOrWhiteSpace(), x => x.LastName != null && x.LastName.Contains(input.LastName))
                .WhereIf(!input.Email.IsNullOrWhiteSpace(), x => x.Email != null && x.Email.Contains(input.Email))
                .WhereIf(!input.Phone.IsNullOrWhiteSpace(), x => x.Phone != null && x.Phone.Contains(input.Phone))
                .WhereIf(!input.Notes.IsNullOrWhiteSpace(), x => x.Notes != null && x.Notes.Contains(input.Notes))
                .WhereIf(input.BirthDate.HasValue, x => x.BirthDate == input.BirthDate.Value)
                .WhereIf(input.SessionBalance.HasValue, x => x.SessionBalance == input.SessionBalance.Value)
                .WhereIf(input.Status.HasValue, x => x.Status == (Status)input.Status.Value)
                .WhereIf(input.FrozenUntil.HasValue, x => x.FrozenUntil == input.FrozenUntil.Value);
        }

        public override async Task<MemberDto> UpdateAsync(MemberDto input)
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
                    EntityType = "Member",
                    EntityId = input.Id.ToString(),
                    FromStatus = fromStatus,
                    ToStatus = toStatus,
                    Action = "Update",
                    ChangedByUserId = AbpSession.UserId
                });
            }

            return await base.UpdateAsync(input);
        }

        [Abp.Authorization.AbpAuthorize(PermissionNames.Member_Update)]
        public async Task<MemberDto> ChangeStatusAsync(long id, ChangeStatusInput input)
        {
            var entity = await Repository.GetAsync(id);
            var currentStatus = entity.Status.ToString();

            // Find valid transition
            var transitions = new (string From, string To, string Action, bool Readonly)[]
            {
            ("Active", "Frozen", "Freeze", false),
            ("Frozen", "Active", "Unfreeze", false),
            ("Active", "Passive", "Deactivate", false),
            ("Frozen", "Passive", "Deactivate", false),
            ("Passive", "Active", "Reactivate", false)
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
                EntityType = "Member",
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
                ("Active", "Frozen"),
                ("Frozen", "Active"),
                ("Active", "Passive"),
                ("Frozen", "Passive"),
                ("Passive", "Active")
            };

            var isValid = allowed.Any(t =>
                (t.From == "*" || t.From == from.ToString()) &&
                t.To == to.ToString());

            if (!isValid)
                throw new Abp.UI.UserFriendlyException($"Invalid status transition from {from} to {to}");
        }
    }
}
