using Abp.Authorization;
using Abp.Localization;

namespace CoachApp.Authorization
{
    public class CoachAppAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var pages = context.GetPermissionOrNull("Pages") ?? context.CreatePermission("Pages", L("Pages"));

            // Coach
            pages.CreateChildPermission(PermissionNames.Coach_Read, L("Coach.Read"));
            pages.CreateChildPermission(PermissionNames.Coach_Create, L("Coach.Create"));
            pages.CreateChildPermission(PermissionNames.Coach_Update, L("Coach.Update"));
            pages.CreateChildPermission(PermissionNames.Coach_Delete, L("Coach.Delete"));

            // Member
            pages.CreateChildPermission(PermissionNames.Member_Read, L("Member.Read"));
            pages.CreateChildPermission(PermissionNames.Member_Create, L("Member.Create"));
            pages.CreateChildPermission(PermissionNames.Member_Update, L("Member.Update"));
            pages.CreateChildPermission(PermissionNames.Member_Delete, L("Member.Delete"));
            pages.CreateChildPermission(PermissionNames.Member_ChangeStatus, L("Member.ChangeStatus"));

            // CoachMember
            pages.CreateChildPermission(PermissionNames.CoachMember_Read, L("CoachMember.Read"));
            pages.CreateChildPermission(PermissionNames.CoachMember_Create, L("CoachMember.Create"));
            pages.CreateChildPermission(PermissionNames.CoachMember_Update, L("CoachMember.Update"));
            pages.CreateChildPermission(PermissionNames.CoachMember_Delete, L("CoachMember.Delete"));

            // SessionPackage
            pages.CreateChildPermission(PermissionNames.SessionPackage_Read, L("SessionPackage.Read"));
            pages.CreateChildPermission(PermissionNames.SessionPackage_Create, L("SessionPackage.Create"));
            pages.CreateChildPermission(PermissionNames.SessionPackage_Update, L("SessionPackage.Update"));
            pages.CreateChildPermission(PermissionNames.SessionPackage_Delete, L("SessionPackage.Delete"));

            // Payment
            pages.CreateChildPermission(PermissionNames.Payment_Read, L("Payment.Read"));
            pages.CreateChildPermission(PermissionNames.Payment_Create, L("Payment.Create"));
            pages.CreateChildPermission(PermissionNames.Payment_Update, L("Payment.Update"));
            pages.CreateChildPermission(PermissionNames.Payment_Delete, L("Payment.Delete"));

            // Lesson
            pages.CreateChildPermission(PermissionNames.Lesson_Read, L("Lesson.Read"));
            pages.CreateChildPermission(PermissionNames.Lesson_Create, L("Lesson.Create"));
            pages.CreateChildPermission(PermissionNames.Lesson_Update, L("Lesson.Update"));
            pages.CreateChildPermission(PermissionNames.Lesson_Delete, L("Lesson.Delete"));
            pages.CreateChildPermission(PermissionNames.Lesson_ChangeStatus, L("Lesson.ChangeStatus"));

            // Attendance
            pages.CreateChildPermission(PermissionNames.Attendance_Read, L("Attendance.Read"));
            pages.CreateChildPermission(PermissionNames.Attendance_Create, L("Attendance.Create"));
            pages.CreateChildPermission(PermissionNames.Attendance_Update, L("Attendance.Update"));
            pages.CreateChildPermission(PermissionNames.Attendance_Delete, L("Attendance.Delete"));

            // RBAC
            pages.CreateChildPermission(PermissionNames.AppUser_Read, L("AppUser.Read"));
            pages.CreateChildPermission(PermissionNames.AppRole_Read, L("AppRole.Read"));
            pages.CreateChildPermission(PermissionNames.AppUser_Create, L("AppUser.Create"));
            pages.CreateChildPermission(PermissionNames.AppRole_Create, L("AppRole.Create"));
            pages.CreateChildPermission(PermissionNames.AppUser_Update, L("AppUser.Update"));
            pages.CreateChildPermission(PermissionNames.AppRole_Update, L("AppRole.Update"));
            pages.CreateChildPermission(PermissionNames.AppUser_Delete, L("AppUser.Delete"));
            pages.CreateChildPermission(PermissionNames.AppRole_Delete, L("AppRole.Delete"));
            pages.CreateChildPermission(PermissionNames.AppRole_AssignPermissions, L("AppRole.AssignPermissions"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, CoachAppConsts.LocalizationSourceName);
        }
    }
}
