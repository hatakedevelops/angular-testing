using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailService.Models
{
    public class ContactFormResponse
    {
        public required int Status {get;set;}
        public required string ResponseMessage {get;set;}
    }
}