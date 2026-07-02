using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CoachApp.EntityFrameworkCore;

namespace CoachApp.Web.Host
{
    [DependsOn(typeof(CoachAppApplicationModule), typeof(CoachAppEntityFrameworkCoreModule), typeof(AbpAspNetCoreModule))]
    public class CoachAppWebHostModule : AbpModule
    {
        public override void PreInitialize()
        {
            // Expose all AppServices as dynamic API controllers
            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(CoachAppApplicationModule).GetAssembly(),
                    moduleName: "app",
                    useConventionalHttpVerbs: true
                );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CoachAppWebHostModule).GetAssembly());
        }
    }
}
