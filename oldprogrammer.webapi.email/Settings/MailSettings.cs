using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oldprogrammer.webapi.email.Settings
{
    public class MailSettings
    {
        public MailSettings(IConfiguration configuration, IWebHostEnvironment env)
        {
            var mailSettingsSection = configuration.GetRequiredSection("MailSettings");

            DisplayName = mailSettingsSection["DisplayName"];
            Email = mailSettingsSection["Email"];
            Host = mailSettingsSection["Host"];
            Port = Convert.ToInt32(mailSettingsSection["Port"]);

            if (env.IsDevelopment())
            {
                Password = mailSettingsSection["FakePassword"];
            }
            else
            {
                Password = configuration["MailSettings:Password"];
            }
        }
        public string Email { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
    }
}
