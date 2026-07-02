namespace CoachApp.Authorization
{
    public static class PermissionNames
    {
        public const string Pages = "Pages";

        // Coach
        public const string Coach_Read = "Coach.Read";
        public const string Coach_Create = "Coach.Create";
        public const string Coach_Update = "Coach.Update";
        public const string Coach_Delete = "Coach.Delete";

        // Member
        public const string Member_Read = "Member.Read";
        public const string Member_Create = "Member.Create";
        public const string Member_Update = "Member.Update";
        public const string Member_Delete = "Member.Delete";
        public const string Member_ChangeStatus = "Member.ChangeStatus";

        // CoachMember
        public const string CoachMember_Read = "CoachMember.Read";
        public const string CoachMember_Create = "CoachMember.Create";
        public const string CoachMember_Update = "CoachMember.Update";
        public const string CoachMember_Delete = "CoachMember.Delete";

        // SessionPackage
        public const string SessionPackage_Read = "SessionPackage.Read";
        public const string SessionPackage_Create = "SessionPackage.Create";
        public const string SessionPackage_Update = "SessionPackage.Update";
        public const string SessionPackage_Delete = "SessionPackage.Delete";

        // Payment
        public const string Payment_Read = "Payment.Read";
        public const string Payment_Create = "Payment.Create";
        public const string Payment_Update = "Payment.Update";
        public const string Payment_Delete = "Payment.Delete";

        // Lesson
        public const string Lesson_Read = "Lesson.Read";
        public const string Lesson_Create = "Lesson.Create";
        public const string Lesson_Update = "Lesson.Update";
        public const string Lesson_Delete = "Lesson.Delete";
        public const string Lesson_ChangeStatus = "Lesson.ChangeStatus";

        // Attendance
        public const string Attendance_Read = "Attendance.Read";
        public const string Attendance_Create = "Attendance.Create";
        public const string Attendance_Update = "Attendance.Update";
        public const string Attendance_Delete = "Attendance.Delete";

        // RBAC management
        public const string AppUser_Read = "AppUser.Read";
        public const string AppRole_Read = "AppRole.Read";
        public const string AppUser_Create = "AppUser.Create";
        public const string AppRole_Create = "AppRole.Create";
        public const string AppUser_Update = "AppUser.Update";
        public const string AppRole_Update = "AppRole.Update";
        public const string AppUser_Delete = "AppUser.Delete";
        public const string AppRole_Delete = "AppRole.Delete";
        public const string AppRole_AssignPermissions = "AppRole.AssignPermissions";

    }
}
