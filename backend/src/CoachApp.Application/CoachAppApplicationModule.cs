using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace CoachApp
{
    [DependsOn(typeof(CoachAppCoreModule), typeof(AbpAutoMapperModule))]
    public class CoachAppApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                cfg.AddMaps(typeof(CoachAppApplicationModule).GetAssembly());
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CoachAppApplicationModule).GetAssembly());
        }
    }
}
