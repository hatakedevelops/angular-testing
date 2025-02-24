using Microsoft.AspNetCore.Mvc;
using EmailService.Models;
using EmailService.services.Interfaces;

namespace EmailService
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        public readonly IEmailService _service;

        public EmailController(IEmailService service)
        {
            _service = service;
        }

        [HttpPost("send")]
        public async Task<IActionResult> Post_SendContactForm([FromBody]ContactForm request) 
        {
            var response = await _service.Post_SendContactFormAsync(request);
            if(request is null)
            {
                return BadRequest();
            }
 
            if(response.Status == 201)
            {
                return CreatedAtAction(nameof(Post_SendContactForm), response);
            }
            return NotFound(response);
        }
    }
}