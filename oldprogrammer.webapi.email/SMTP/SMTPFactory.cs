using MailKit.Net.Smtp;
using MimeKit;
using oldprogrammer.webapi.email.Settings;

namespace oldprogrammer.webapi.email.SMTP
{
    public class SMTPFactory : ISMTPFactory
    {
        private readonly MailSettings _mailSettings;
        private readonly ILogger<SMTPFactory> _logger;
        public SMTPFactory(IConfiguration configuration, MailSettings mailSettings, ILogger<SMTPFactory> logger)
        {
            _mailSettings = mailSettings;
            _logger = logger;
        }

        public async Task<SmtpClient> GenerateDefaultSMTPClient()
        {
            SmtpClient smtpClient = new();

            await smtpClient.ConnectAsync(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            await smtpClient.AuthenticateAsync(_mailSettings.Email, _mailSettings.Password);

            return smtpClient;
        }

        public async Task SendMessageWithDefaultSMTPClient(MimeMessage message)
        {
            try
            {
                _logger.LogInformation("Trying to send and connect smtp client, smtp:{SMTP}", _mailSettings.Host);
                using var smtpClient = await GenerateDefaultSMTPClient();
                await smtpClient.SendAsync(message);
                await smtpClient.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An ocurred happen, method: SendMessageWithDefaultSMTPClient, class: SMTPFactory, MessageException:{Message}", ex.Message);
                throw ex;
            }
        }
    }
}
