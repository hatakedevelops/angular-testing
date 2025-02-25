using System.Net;
using System.Net.Mail;

using EmailService.Models;
using EmailService.services.Interfaces;
using Microsoft.Extensions.Options;

namespace EmailService.services.Implementations
{
    public class EmailServiceHelper : IEmailService
    {
        private readonly Smtp _smtp;

        public EmailServiceHelper(IOptions<Smtp> smtp)
        {
            _smtp = smtp.Value;
        }
        public async Task<ContactFormResponse> Post_SendContactFormAsync(ContactForm request)
        {
            using (var client = new SmtpClient(_smtp.Host, _smtp.Port))
            {
                client.Credentials = new NetworkCredential(_smtp.Username, _smtp.Password);
                client.EnableSsl = true;

                var mail = new MailMessage
                {
                    From = new MailAddress(_smtp.Username),
                    Subject = request.Subject,
                    Body = $"New Message From {request.FirstName} {request.LastName}\r\nMessage:\r\n{request.Message}",
                    IsBodyHtml = true
                };

                mail.To.Add(request.Email);

                try
                {
                    await client.SendMailAsync(mail);
                    return new ContactFormResponse
                    {
                        Status = 201,
                        ResponseMessage = "Email has been delivered."
                    };
                }
                catch (Exception ex)
                {
                    return new ContactFormResponse
                    {
                        Status = 500,
                        ResponseMessage = $"Failed to send email.\r\nError Code: {ex.HResult} \r\nMessage:{ex.Message}"
                    };
                }
            }
        }
    }
}