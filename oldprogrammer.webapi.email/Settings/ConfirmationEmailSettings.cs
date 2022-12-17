namespace oldprogrammer.webapi.email.Settings
{
    public class ConfirmationEmailSettings
    {
        public ConfirmationEmailSettings(IConfiguration configuration) 
        {
            var conformationEmailSettingsSection = configuration.GetRequiredSection("ConfirmationEmailSettings");

            Subject = conformationEmailSettingsSection["Subject"];
            ConfirmationEmailTemplate = conformationEmailSettingsSection["ConfirmationEmailTemplate"];
        }

        public string Subject { get; set; }
        public string ConfirmationEmailTemplate { get; set; }
    }
}
