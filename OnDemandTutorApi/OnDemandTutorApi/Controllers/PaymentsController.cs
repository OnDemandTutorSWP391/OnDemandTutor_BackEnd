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

        //[HttpGet("forward-response")]
        //public IActionResult ForwardResponse()
        //{
        //    string url = $"http://localhost:5173/payment-result/api/Payments/response-payment?vnp_Amount=100000000&vnp_BankCode=NCB&vnp_BankTranNo=VNP14463810&vnp_CardType=ATM&vnp_OrderInfo=Thanh+to%C3%A1n+cho+%C4%91%C6%A1n+h%C3%A0ng%3A+638542470816802491&vnp_PayDate=20240617185205&vnp_ResponseCode=00&vnp_TmnCode=247D8ZMX&vnp_TransactionNo=14463810&vnp_TransactionStatus=00&vnp_TxnRef=638542470816802491&vnp_SecureHash=19aa2e0298db24669e171312cf4e7072e4986ca1733f1f3d26346d70dfeb0903b78f23c64b1ba4dd5ad68ed254e2e14f7526629166e824295331e19eb38b4d4a";
        //    // Forward request đến API ResponsePayment
        //    return RedirectToAction(url);
        //}


        //public bool Success { get; set; }
        //public string PaymentMethod { get; set; }
        //public string OrderDescription { get; set; }
        //public string OrderId { get; set; }
        //public string TransactionId { get; set; }
        //public string Token { get; set; }
        //public string VnPayResponseCode { get; set; }
        //public string Message { get; set; }
        //public float Amount { get; set; }
    }
}
