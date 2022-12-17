using MailKit.Net.Smtp;
using MimeKit;

namespace oldprogrammer.webapi.email.SMTP
{
    public interface ISMTPFactory
    {
        Task<SmtpClient> GenerateDefaultSMTPClient();
        Task SendMessageWithDefaultSMTPClient(MimeMessage message);
    }
}
