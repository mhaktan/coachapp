using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CoachApp.EntityFrameworkCore
{
    [DependsOn(typeof(CoachAppCoreModule), typeof(AbpEntityFrameworkCoreModule))]
    public class CoachAppEntityFrameworkCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpEfCore().AddDbContext<CoachAppDbContext>(options =>
            {
                // Read connection string from appsettings.json + env vars.
                // AddEnvironmentVariables() ŞART: Railway/Docker ConnectionStrings__Default env var'ını
                // okur ve appsettings.json'daki statik (localhost) değeri override eder. Yoksa ABP
                // DbContext (seed dahil) localhost'a bağlanmaya çalışır → seed fail, admin oluşmaz.
                var config = new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false)
                    .AddEnvironmentVariables()
                    .Build();
                var connStr = config.GetConnectionString("Default");
                options.DbContextOptions.UseNpgsql(connStr);
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CoachAppEntityFrameworkCoreModule).GetAssembly());
        }
    }
}
