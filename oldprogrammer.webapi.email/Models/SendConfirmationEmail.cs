namespace oldprogrammer.webapi.email.Models
{
    public class SendConfirmationEmail
    {
        public SendConfirmationEmail()
        {
        }
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
