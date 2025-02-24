using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using EmailService.Models;
using EmailService.services.Interfaces;

namespace EmailService
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;
        private readonly LogLevel _logLevel;
        public readonly IEmailService _service;

        public EmailController(ILogger<EmailController> logger, LogLevel logLevel, IEmailService service)
        {
            _logger = logger;
            _logLevel = logLevel;
            _service = service;
        }

        [HttpPost]
        public ActionResult<ContactFormResponse> Post_SendContactForm([FromBody]ContactForm request) 
        {
            if(request == null)
            {
                return BadRequest();
            }
            else 
            {
                _logger.Log(_logLevel, "Sending Client Request");
                var response = _service.Post_SendContactForm(request);
                if(response != null)
                {
                    _logger.Log(_logLevel, "Response Received.");
                    return Ok(response);
                }
            }
            return null;
        }
    }
}