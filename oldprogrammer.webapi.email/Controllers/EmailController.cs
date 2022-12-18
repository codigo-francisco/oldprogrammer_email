using Microsoft.AspNetCore.Mvc;
using oldprogrammer.webapi.email.Emails;
using oldprogrammer.webapi.email.Models;

namespace oldprogrammer.webapi.email.Controllers
{
    [Route("api/email")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSystem _emailSystem;
        private ILogger<EmailController> _logger;
        public EmailController(IEmailSystem emailSystem, ILogger<EmailController> logger)
        {
            _emailSystem = emailSystem;
            _logger = logger;
        }
        [HttpPost("sendconfirmation")]
        public async Task<IActionResult> SendConfirmation([FromBody] SendConfirmationEmail sendConfirmationEmail)
        {
            try
            {
                _logger.LogInformation("System tries to send email in EmailController");

                await _emailSystem.SendEmailConfirmationAsync(sendConfirmationEmail.Email, sendConfirmationEmail.Token);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred method: SendConfirmation, class: EmailController, ExceptionMessage: {Message}", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("ping")]
        public string Ping()
        {
            return "EmailController is working";
        }
    }
}
