using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CoachApp.EntityFrameworkCore;
using CoachApp.EntityFrameworkCore.Seed;
using CoachApp.Entities;

namespace CoachApp.Web.Host
{
    /// <summary>
    /// Background service that runs migration + seed once at startup
    /// without blocking the HTTP pipeline.
    /// </summary>
    public class MigrationHostedService : IHostedService
    {
        private readonly IConfiguration _config;

        public MigrationHostedService(IConfiguration config)
        {
            _config = config;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var connStr = _config.GetConnectionString("Default") ?? "";
            if (string.IsNullOrEmpty(connStr)) return;

            // Run in background to avoid blocking host startup (prevents EF tooling timeout)
            _ = Task.Run(async () =>
            {
                // Small delay to ensure host is fully started before DB operations
                await Task.Delay(1000, cancellationToken);
                try
                {
                    var optionsBuilder = new DbContextOptionsBuilder();
                    optionsBuilder.UseNpgsql(connStr);

                    using (var db = new CoachAppDbContext(optionsBuilder.Options))
                    {
                        db.Database.EnsureCreated();
                        Console.WriteLine("[Migration] Database is up to date.");

                        // Seed sample data — wrapped in its own try so a failure here doesn't block RBAC seed below.
                        try
                        {
                    if (!db.Coaches.Any())
                    {
                        db.Coaches.AddRange(
                    new Coach { Id = 1, FirstName = "Alice Johnson", LastName = "Alice Johnson", Email = "alice@example.com", Phone = "+1-555-0101", Specialization = "Sample Item 1", IsActive = true },
                    new Coach { Id = 2, FirstName = "Bob Smith", LastName = "Bob Smith", Email = "bob@example.com", Phone = "+1-555-0102", Specialization = "Sample Item 2", IsActive = false }
                        );
                    }
                    if (!db.Members.Any())
                    {
                        db.Members.AddRange(
                    new Member { Id = 3, FirstName = "Alice Johnson", LastName = "Alice Johnson", Email = "alice@example.com", Phone = "+1-555-0101", BirthDate = new DateTime(2024, 3, 15), SessionBalance = 42, Status = (Status)0, FrozenUntil = new DateTime(2024, 3, 15), Notes = "Lorem ipsum dolor sit amet" },
                    new Member { Id = 4, FirstName = "Bob Smith", LastName = "Bob Smith", Email = "bob@example.com", Phone = "+1-555-0102", BirthDate = new DateTime(2024, 6, 20), SessionBalance = 17, Status = (Status)1, FrozenUntil = new DateTime(2024, 6, 20), Notes = "Consectetur adipiscing elit" }
                        );
                    }
                    if (!db.CoachMembers.Any())
                    {
                        db.CoachMembers.AddRange(
                    new CoachMember { Id = 5, AssignedAt = new DateTime(2024, 3, 15), IsActive = true, MemberId = 3, CoachId = 1 },
                    new CoachMember { Id = 6, AssignedAt = new DateTime(2024, 6, 20), IsActive = false, MemberId = 4, CoachId = 2 }
                        );
                    }
                    if (!db.SessionPackages.Any())
                    {
                        db.SessionPackages.AddRange(
                    new SessionPackage { Id = 7, PackageName = "Alice Johnson", SessionCount = 42, Price = 99.99m, IsActive = true },
                    new SessionPackage { Id = 8, PackageName = "Bob Smith", SessionCount = 17, Price = 149.50m, IsActive = false }
                        );
                    }
                    if (!db.Payments.Any())
                    {
                        db.Payments.AddRange(
                    new Payment { Id = 9, Amount = 99.99m, PaymentDate = new DateTime(2024, 3, 15), PaymentMethod = (PaymentMethod)0, SessionsPurchased = 42, Notes = "Lorem ipsum dolor sit amet", MemberId = 3, SessionPackageId = 7 },
                    new Payment { Id = 10, Amount = 149.50m, PaymentDate = new DateTime(2024, 6, 20), PaymentMethod = (PaymentMethod)1, SessionsPurchased = 17, Notes = "Consectetur adipiscing elit", MemberId = 4, SessionPackageId = 8 }
                        );
                    }
                    if (!db.Lessons.Any())
                    {
                        db.Lessons.AddRange(
                    new Lesson { Id = 11, LessonDate = new DateTime(2024, 3, 15), StartTime = "Sampl", EndTime = "Sampl", Title = "Introduction to Physics", Notes = "Lorem ipsum dolor sit amet", Status = (Status)0, CoachId = 1 },
                    new Lesson { Id = 12, LessonDate = new DateTime(2024, 6, 20), StartTime = "Sampl", EndTime = "Sampl", Title = "Advanced Mathematics", Notes = "Consectetur adipiscing elit", Status = (Status)1, CoachId = 2 }
                        );
                    }
                    if (!db.Attendances.Any())
                    {
                        db.Attendances.AddRange(
                    new Attendance { Id = 13, Attended = true, SessionDeducted = true, Notes = "Lorem ipsum dolor sit amet", LessonId = 11, MemberId = 3 },
                    new Attendance { Id = 14, Attended = false, SessionDeducted = false, Notes = "Consectetur adipiscing elit", LessonId = 12, MemberId = 4 }
                        );
                    }
                            db.SaveChanges();
                            Console.WriteLine("[Seed] Sample data created.");
                        }
                        catch (Exception sampleEx)
                        {
                            Console.WriteLine($"[Seed] Sample data skipped: {sampleEx.GetType().Name}: {sampleEx.Message}");
                            // Carry on — RBAC seed must still run so admin/123qwe is usable.
                        }
                    }
                    // RBAC seed (Admin/User roles + permissions + admin user) runs through ABP DI
                    // so PermissionRegistry can be injected. SeedHelper is idempotent.
                    SeedHelper.SeedHostDb(Abp.Dependency.IocManager.Instance);
                    Console.WriteLine("[Seed] RBAC seed complete (Admin role + admin user).");
                }
                catch (Exception ex)
                {
                    // Full diagnostic — surface the real cause so silent seed failures are debuggable.
                    Console.WriteLine($"[Migration] FAILED: {ex.GetType().Name}: {ex.Message}");
                    if (ex.InnerException != null)
                        Console.WriteLine($"[Migration] InnerException: {ex.InnerException.GetType().Name}: {ex.InnerException.Message}");
                    Console.WriteLine("[Migration] StackTrace:");
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine("[Migration] App continues without migration — admin user will not exist.");
                }
            }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }

    public class Program
    {
        // Runtime entry: WebHost is required because ABP Startup returns IServiceProvider.
        public static void Main(string[] args)
        {
            // Npgsql 7+ requires UTC DateTimes — enable legacy behavior for ABP compatibility
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build()
                .Run();
        }

        // Design-time entry for EF Core tools (dotnet ef migrations).
        // Without this, EF tools wait 5 minutes for IHost build (resolver default timeout)
        // and then SIGTERM any running dotnet process — killing live dev servers.
        // We expose a minimal IHost that EF tools resolve in milliseconds; the actual
        // DbContext is built by IDesignTimeDbContextFactory in the EntityFrameworkCore project.
        public static IHostBuilder CreateHostBuilder(string[] args)
            => Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args);
    }
}
