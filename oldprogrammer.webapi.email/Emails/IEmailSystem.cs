using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oldprogrammer.webapi.email.Emails
{
    public interface IEmailSystem
    {
        Task SendEmailConfirmationAsync(string email, string token);
    }
}
