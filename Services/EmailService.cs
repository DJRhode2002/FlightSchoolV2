using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services; // IMPORTANT: Required for Identity

namespace FlightSchoolV2.Services
{
    // Adding IEmailSender here is what fixes the Program.cs error
    public class EmailService : IEmailService, IEmailSender
    {
        // 1. This handles your custom notifications (from IEmailService)
        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            await Execute(toEmail, subject, message);
        }

        // 2. This handles Identity links (from IEmailSender)
        // Note: The parameter names must match the interface exactly
        async Task IEmailSender.SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await Execute(email, subject, htmlMessage);
        }

        // 3. This is the single "Engine" that actually sends the mail
        private async Task Execute(string toEmail, string subject, string message)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                // TIP: If this fails, use a Google "App Password" instead of your login password
                Credentials = new NetworkCredential("yamithethinker@gmail.com", "idzqygackdydvajx")
            };

            var mailMessage = new MailMessage("yamithethinker@gmail.com", toEmail, subject, message)
            {
                IsBodyHtml = true
            };

            await client.SendMailAsync(mailMessage);
        }
    }
}

//GoFuckYourself@18