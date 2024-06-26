using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailsController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send-mail")]
        public IActionResult SendMail(SendEmailRequestDTO sendEmailRequest)
        {
            var title = $"{sendEmailRequest.Subject}";
            var content = $@"
<p>- {sendEmailRequest.Body}</p>";

            var message = new EmailDTO
                (
                    sendEmailRequest.ToEmails,
                        title,
                        content!
                );
            _emailService.SendEmail(message);

            return Ok();
        }
    }
}
