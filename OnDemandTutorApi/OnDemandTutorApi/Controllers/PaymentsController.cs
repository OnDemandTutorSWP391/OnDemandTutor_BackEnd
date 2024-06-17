using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl;
using System.Net;

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
                string urlFailed = $"http://localhost:5173/payment-result?" +
                                   $"success={WebUtility.UrlEncode(vnPayResponse.Success.ToString())}" +
                                   $"&message={WebUtility.UrlEncode(vnPayResponse.Message)}";
                return Redirect(urlFailed);
            }

            string urlSuccess = $"http://localhost:5173/payment-result?" +
                         $"success={WebUtility.UrlEncode(vnPayResponse.Success.ToString())}" +
                         $"&paymentMethod={WebUtility.UrlEncode(vnPayResponse.Data.PaymentMethod)}" +
                         $"&orderDescription={WebUtility.UrlEncode(vnPayResponse.Data.OrderDescription)}" +
                         $"&orderId={WebUtility.UrlEncode(vnPayResponse.Data.OrderId)}" +
                         $"&transactionId={WebUtility.UrlEncode(vnPayResponse.Data.TransactionId)}" +
                         $"&token={WebUtility.UrlEncode(vnPayResponse.Data.Token)}" +
                         $"&vnPayResponseCode={WebUtility.UrlEncode(vnPayResponse.Data.VnPayResponseCode)}" +
                         $"&message={WebUtility.UrlEncode(vnPayResponse.Message)}" +
                         $"&amount={WebUtility.UrlEncode(vnPayResponse.Data.Amount.ToString())}";

            return Redirect(urlSuccess);
        }

        
            //[HttpGet("response-payment")]
            //public IActionResult ResponsePayment()
            //{
            //    var vnPayResponse = _vnPayService.PaymentExcute(Request.Query);

            //    if(!vnPayResponse.Success)
            //    {
            //    return BadRequest(vnPayResponse);
            //    }

            //    return Ok(vnPayResponse);
            //} 

         
    }
}
