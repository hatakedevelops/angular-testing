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

        public EmailServiceHelper(SmtpClient client)
        {
            _client = client;
        }
        public async Task<ContactFormResponse> Post_SendContactFormAsync(ContactForm request)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(request.Email), // Changed to User Secrets
                // add a To = request.Email
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
                    ResponseMessage = "Email has been successfuly delivered"
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