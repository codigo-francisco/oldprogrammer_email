namespace oldprogrammer.webapi.email.Templates
{
    public class ConfirmationEmailTemplate : IConfirmationEmailTemplate
    {
        private readonly string templateContent;
        public ConfirmationEmailTemplate(IConfiguration configuration, IWebHostEnvironment webHostEnvironment) 
        {
            var confirmationEmailTempalteFilePath = configuration.GetRequiredSection("ConfirmationEmailSettings")["ConfirmationEmailTemplate"];

            var templateFileInfo = webHostEnvironment.WebRootFileProvider.GetFileInfo(confirmationEmailTempalteFilePath);

            using var sr = new StreamReader(templateFileInfo.CreateReadStream());
            templateContent = sr.ReadToEnd();
            sr.Dispose();
        }
        public string GetTemplate()
        {
            return templateContent;
        }
    }
}
