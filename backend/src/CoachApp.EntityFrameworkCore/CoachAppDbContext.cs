using Microsoft.EntityFrameworkCore;
using Abp.EntityFrameworkCore;
using CoachApp.Entities;

namespace CoachApp.EntityFrameworkCore
{
    public class CoachAppDbContext : AbpDbContext
    {
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<CoachMember> CoachMembers { get; set; }
        public DbSet<SessionPackage> SessionPackages { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<StatusChangeLog> StatusChangeLogs { get; set; }


        public CoachAppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Member 1:N CoachMember
            modelBuilder.Entity<CoachMember>()
                .HasOne(x => x.Member)
                .WithMany(x => x.CoachMembers)
                .HasForeignKey(x => x.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            // Coach 1:N CoachMember
            modelBuilder.Entity<CoachMember>()
                .HasOne(x => x.Coach)
                .WithMany(x => x.CoachMembers)
                .HasForeignKey(x => x.CoachId)
                .OnDelete(DeleteBehavior.Restrict);

            // Member 1:N Payment
            modelBuilder.Entity<Payment>()
                .HasOne(x => x.Member)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            // SessionPackage 1:N Payment
            modelBuilder.Entity<Payment>()
                .HasOne(x => x.SessionPackage)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.SessionPackageId)
                .OnDelete(DeleteBehavior.Restrict);

            // Coach 1:N Lesson
            modelBuilder.Entity<Lesson>()
                .HasOne(x => x.Coach)
                .WithMany(x => x.Lessons)
                .HasForeignKey(x => x.CoachId)
                .OnDelete(DeleteBehavior.Restrict);

            // Lesson 1:N Attendance
            modelBuilder.Entity<Attendance>()
                .HasOne(x => x.Lesson)
                .WithMany(x => x.Attendances)
                .HasForeignKey(x => x.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            // Member 1:N Attendance
            modelBuilder.Entity<Attendance>()
                .HasOne(x => x.Member)
                .WithMany(x => x.Attendances)
                .HasForeignKey(x => x.MemberId)
                .OnDelete(DeleteBehavior.Restrict);


            // RBAC: AppUser N:N AppRole via UserRole junction
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserRole>()
                .HasIndex(ur => new { ur.UserId, ur.RoleId })
                .IsUnique();

            // RolePermission: AppRole 1:N RolePermission
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<RolePermission>()
                .HasIndex(rp => new { rp.RoleId, rp.PermissionName })
                .IsUnique();

            // AppRole.Name unique
            modelBuilder.Entity<AppRole>()
                .HasIndex(r => r.Name)
                .IsUnique();

        }
    }
}
