using Abp.Modules;
using Abp.Reflection.Extensions;

namespace CoachApp
{
    public class CoachAppCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = CoachAppConsts.ConnectionStringName;
            // SMTP: ABP reads email settings from AbpSettings table by default.
            // Override via ISmtpEmailSenderConfiguration registration if needed.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CoachAppCoreModule).GetAssembly());
        }
    }
}
