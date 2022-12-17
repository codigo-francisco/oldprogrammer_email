using oldprogrammer.webapi.email.Emails;
using oldprogrammer.webapi.email.Settings;
using oldprogrammer.webapi.email.SMTP;
using oldprogrammer.webapi.email.Templates;

namespace oldprogrammer.webapi.email
{
    public static class Services
    {
        public static void ConfigureDependencies(this IServiceCollection services)
        {
            services.AddSingleton<ConfirmationEmailSettings>();
            services.AddSingleton<MailSettings>();
            services.AddSingleton<IConfirmationEmailTemplate, ConfirmationEmailTemplate>();

            services.AddScoped<ISMTPFactory, SMTPFactory>();
            services.AddScoped<IEmailSystem, EmailSystem>();
        }
    }
}
