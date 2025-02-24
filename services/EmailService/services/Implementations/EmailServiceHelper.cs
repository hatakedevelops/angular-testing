using System.Net;
using System.Net.Mail;

using EmailService.Models;
using EmailService.services.Interfaces;
using EmailService.services.Constants;

namespace EmailService.services.Implementations
{
    public class EmailServiceHelper : IEmailService
    {

        private readonly SmtpClient _client;

        public EmailServiceHelper(SmtpClient client, IConfiguration configuration)
        {
            var smtpSection = configuration.GetSection("Smtp");
            _client = new SmtpClient
            {
                Port = smtpSection.GetValue<int>("Port"),
                Host = smtpSection.GetValue<string>("Host")!,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(
                    smtpSection.GetValue<string>("Username"),
                    smtpSection.GetValue<string>("Password"))
            };
        }
        public async Task<ContactFormResponse> Post_SendContactForm(ContactForm request)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("{0}", request.Email),
                Subject = request.Subject,
                Body = $"New Message From {request.FirstName} {request.LastName}\r\nMessage:\r\n{request.Message}",
                IsBodyHtml = true
            };

            mailMessage.To.Add(RecipientAddresses.TestEmail);

            try
            {
                await _client.SendMailAsync(mailMessage);
                return new ContactFormResponse
                {
                    Status = 200,
                    ResponseMessage = "Email sent successfully!"
                };
            }
            catch (Exception ex)
            {
                return new ContactFormResponse
                {
                    Status = ex.HResult,
                    ResponseMessage = $"Failed to send email: {ex.Message}"
                };
            }
        }
    }
}