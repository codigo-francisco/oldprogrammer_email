using Microsoft.Extensions.Configuration;
using MimeKit;
using oldprogrammer.webapi.email.Settings;
using oldprogrammer.webapi.email.SMTP;
using oldprogrammer.webapi.email.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oldprogrammer.webapi.email.Emails
{
    public class EmailSystem : IEmailSystem
    {
        private readonly ISMTPFactory _smtpFactory;
        private readonly MailSettings _mailSettings;
        private readonly ConfirmationEmailSettings _confirmationEmailSettings;
        private readonly string _urlConfirmationEmailToken = string.Empty;
        private readonly string _confirmationEmailContent = string.Empty;
        private readonly ILogger<EmailSystem> _logger;

        public EmailSystem(IWebHostEnvironment env, ISMTPFactory smtpFactory, MailSettings mailSettings, 
            ConfirmationEmailSettings confirmationEmailSettings, IConfirmationEmailTemplate confirmationEmailTemplate,
            IConfiguration configuration, ILogger<EmailSystem> logger)
        {
            _smtpFactory = smtpFactory;
            _mailSettings = mailSettings;
            _confirmationEmailSettings = confirmationEmailSettings;
            _confirmationEmailContent = confirmationEmailTemplate.GetTemplate();
            _urlConfirmationEmailToken = configuration.GetSection("Endpoints")["ConfirmationEmailToken"];
            _logger= logger;
        }

        public async Task SendEmailConfirmationAsync(string email, string token)
        {
            try
            {
                _logger.LogInformation("Email systems tries to send email to {Email}", email);
                //Generate link
                var tokenGenerated = string.Format(_urlConfirmationEmailToken, email, token);

                //Generate template with link
                var templateGenerated = string.Format(_confirmationEmailContent, tokenGenerated);

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Email));
                message.To.Add(new MailboxAddress(email, email));
                message.Subject = _confirmationEmailSettings.Subject;
                message.Body = new BodyBuilder
                {
                    HtmlBody = templateGenerated
                }.ToMessageBody();

                await _smtpFactory.SendMessageWithDefaultSMTPClient(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error courred method: SendEmailConfirmationAsync, class: EmailSystems, ExceptionMessage: {Message}", ex.Message);
            }
        }
    }
}
