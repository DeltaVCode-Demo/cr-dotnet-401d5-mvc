using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMvc.Services
{
    public interface IEmailService
    {
        Task SendEmail(string to, string subject, string bodyPlain, string bodyHtml);
    }

    public class SendGridEmailService : IEmailService
    {
        private readonly ILogger<SendGridEmailService> logger;

        private IConfiguration Configuration { get; }

        public SendGridEmailService(IConfiguration configuration, ILogger<SendGridEmailService> logger)
        {
            Configuration = configuration;
            this.logger = logger;
        }

        public async Task SendEmail(string toEmail, string subject, string plainTextContent, string htmlContent)
        {
            var apiKey = Configuration["SendGrid:ApiKey"]
                ?? throw new InvalidOperationException("SendGrid:ApiKey not found!");

            var client = new SendGridClient(apiKey);
            //var from = new EmailAddress("test@example.com", "Example User");
            var from = new EmailAddress("keith+401d5@deltavcodeschool.com");
            //var subject = "Sending with SendGrid is Fun";
            // var to = new EmailAddress("test@example.com", "Example User");
            var to = new EmailAddress(toEmail);
            //var plainTextContent = "and easy to do anywhere, even with C#";
            //var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            if (!response.IsSuccessStatusCode)
            {
                string responseBody = await response.Body.ReadAsStringAsync();
                // TODO: Include more info to troubleshoot
                logger.LogWarning(
                    "Could not send email! {Status} {Body}",
                    response.StatusCode,
                    responseBody);
            }
        }
    }
}
