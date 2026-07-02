using System.Collections.Generic;
using Abp.Dependency;

namespace CoachApp.Authorization
{
    /// <summary>Single permission descriptor — name, group (entity), description.</summary>
    public class PermissionInfo
    {
        public string Name { get; }
        public string Group { get; }
        public string Description { get; }
        public bool IsRbac { get; }

        public PermissionInfo(string name, string group, string description, bool isRbac)
        {
            Name = name; Group = group; Description = description; IsRbac = isRbac;
        }
    }

    public interface IPermissionRegistry
    {
        IReadOnlyList<PermissionInfo> All { get; }
    }

    public class PermissionRegistry : IPermissionRegistry, ISingletonDependency
    {
        public IReadOnlyList<PermissionInfo> All { get; } = new List<PermissionInfo>
        {
            new PermissionInfo("Coach.Read", "Coach", "Read Coach", false),
            new PermissionInfo("Coach.Create", "Coach", "Create Coach", false),
            new PermissionInfo("Coach.Update", "Coach", "Update Coach", false),
            new PermissionInfo("Coach.Delete", "Coach", "Delete Coach", false),
            new PermissionInfo("Member.Read", "Member", "Read Member", false),
            new PermissionInfo("Member.Create", "Member", "Create Member", false),
            new PermissionInfo("Member.Update", "Member", "Update Member", false),
            new PermissionInfo("Member.Delete", "Member", "Delete Member", false),
            new PermissionInfo("Member.ChangeStatus", "Member", "Change Member status", false),
            new PermissionInfo("CoachMember.Read", "CoachMember", "Read CoachMember", false),
            new PermissionInfo("CoachMember.Create", "CoachMember", "Create CoachMember", false),
            new PermissionInfo("CoachMember.Update", "CoachMember", "Update CoachMember", false),
            new PermissionInfo("CoachMember.Delete", "CoachMember", "Delete CoachMember", false),
            new PermissionInfo("SessionPackage.Read", "SessionPackage", "Read SessionPackage", false),
            new PermissionInfo("SessionPackage.Create", "SessionPackage", "Create SessionPackage", false),
            new PermissionInfo("SessionPackage.Update", "SessionPackage", "Update SessionPackage", false),
            new PermissionInfo("SessionPackage.Delete", "SessionPackage", "Delete SessionPackage", false),
            new PermissionInfo("Payment.Read", "Payment", "Read Payment", false),
            new PermissionInfo("Payment.Create", "Payment", "Create Payment", false),
            new PermissionInfo("Payment.Update", "Payment", "Update Payment", false),
            new PermissionInfo("Payment.Delete", "Payment", "Delete Payment", false),
            new PermissionInfo("Lesson.Read", "Lesson", "Read Lesson", false),
            new PermissionInfo("Lesson.Create", "Lesson", "Create Lesson", false),
            new PermissionInfo("Lesson.Update", "Lesson", "Update Lesson", false),
            new PermissionInfo("Lesson.Delete", "Lesson", "Delete Lesson", false),
            new PermissionInfo("Lesson.ChangeStatus", "Lesson", "Change Lesson status", false),
            new PermissionInfo("Attendance.Read", "Attendance", "Read Attendance", false),
            new PermissionInfo("Attendance.Create", "Attendance", "Create Attendance", false),
            new PermissionInfo("Attendance.Update", "Attendance", "Update Attendance", false),
            new PermissionInfo("Attendance.Delete", "Attendance", "Delete Attendance", false),
            new PermissionInfo("AppUser.Read", "AppUser", "Read users", true),
            new PermissionInfo("AppRole.Read", "AppRole", "Read roles", true),
            new PermissionInfo("AppUser.Create", "AppUser", "Create users", true),
            new PermissionInfo("AppRole.Create", "AppRole", "Create roles", true),
            new PermissionInfo("AppUser.Update", "AppUser", "Update users", true),
            new PermissionInfo("AppRole.Update", "AppRole", "Update roles", true),
            new PermissionInfo("AppUser.Delete", "AppUser", "Delete users", true),
            new PermissionInfo("AppRole.Delete", "AppRole", "Delete roles", true),
            new PermissionInfo("AppRole.AssignPermissions", "AppRole", "Assign permissions to roles", true),
        };
    }
}
