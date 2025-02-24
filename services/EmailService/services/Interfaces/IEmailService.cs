using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailService.Models;

namespace EmailService.services.Interfaces
{
    public interface IEmailService
    {
        public Task<ContactFormResponse> Post_SendContactForm(ContactForm request);
    }
}