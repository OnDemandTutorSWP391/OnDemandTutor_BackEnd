using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl;

namespace OnDemandTutorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;

        public PaymentsController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        [HttpPost("request-payment")]
        public IActionResult RequestPayment(VnPayRequestDTO vnPayRequest)
        {
            var paymentUrl = _vnPayService.CreatePaymentUrl(HttpContext, vnPayRequest);

            return Ok(new { Url = paymentUrl });
        }

        [HttpGet("response-payment")]
        public IActionResult ResponsePayment()
        {
            var vnPayResponse = _vnPayService.PaymentExcute(Request.Query);

            if(!vnPayResponse.Success)
            {
                return BadRequest(vnPayResponse);
            }

            return Ok(vnPayResponse);
        }
    }
}
