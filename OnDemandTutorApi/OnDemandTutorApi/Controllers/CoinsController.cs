using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl;
using OnDemandTutorApi.DataAccessLayer.DAO;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;
using System.Security.Claims;

namespace OnDemandTutorApi.Controllers
{
    [Authorize(Roles = "Student, Tutor")]
    [Route("api/[controller]")]
    [ApiController]
    public class CoinsController : ControllerBase
    {
        private readonly ICoinManagementService _coinManagementService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserManager<User> _userManager;

        public CoinsController(ICoinManagementService coinManagementService, IHttpClientFactory httpClientFactory, UserManager<User> userManager)
        {
            _coinManagementService = coinManagementService;
            _httpClientFactory = httpClientFactory;
            _userManager = userManager;
        }

        [HttpPost("Deposit")]
        public async Task<IActionResult> DepositAsync(float coin)
        {
            var userId = HttpContext.User.FindFirstValue("Id");

            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
            {
                return NotFound("Error when identify user. Please sign in and try again.");
            }

            //gọi endpoint PaymentRequest
            var paymentRequest = new VnPayRequestDTO
            {
                FullName = user.FullName,
                Amount = coin * 1000,
                Description = $"{user.FullName} - {user.PhoneNumber}",
            };


            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.PostAsJsonAsync("https://localhost:7259/api/Payments/request-payment", paymentRequest);

            if(!response.IsSuccessStatusCode)
            {
                return BadRequest("Error to send payment url. Please try again.");
            }

            var paymentResponse = await response.Content.ReadFromJsonAsync<PaymentResponseDTO>();

            return Ok(new {PaymentUrl = paymentResponse.Url});
        }

        [HttpPost("ConfirmDeposit")]
        public async Task<IActionResult> ConfirmDepositAsync([FromBody] ResponseApiDTO<VnPayResponseDTO> vnPayResponse)
        {
            var userId = HttpContext.User.FindFirstValue("Id");

            if(!vnPayResponse.Success || !vnPayResponse.Data.VnPayResponseCode.Equals("00"))
            {
                return BadRequest($"{vnPayResponse.Message}");
            }

            // Add record to db
            var result = await _coinManagementService.DepositAsync(new CoinDTO 
            { 
                UserId = userId, 
                Coin = (vnPayResponse.Data.Amount / 100000), 
                TransactionId = Convert.ToInt64(vnPayResponse.Data.TransactionId)
            });

            if(!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("transfer-coin")]
        public async Task<IActionResult> TransferAsync(string receiverId, float coin)
        {
            var userId = HttpContext.User.FindFirstValue("Id");
            var result = await _coinManagementService.TransferAsync(userId, receiverId, coin);
            if(!result.Success) 
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("get-total-coin")]
        public async Task<IActionResult> GetTotalCoinAsync()
        {
            var userId = HttpContext.User.FindFirstValue("Id");

            var result = await _coinManagementService.GetTotalCoinForUserAsync(userId);

            if(!result.Success)
            {
                return BadRequest("Hệ thống gặp lỗi khi hiển thị tổng số dư của bạn.");
            }

            return Ok(result);
        }

        [HttpGet("get-transaction-for-user")]
        public async Task<IActionResult> GetTransactionForUserAsync(DateTime? from, DateTime? to, string? sortBy, int page = 1)
        {
            var userId = HttpContext.User.FindFirstValue("Id"); 

            var result = await _coinManagementService.GetTransactionForUserAsync(userId, from, to, sortBy, page);

            if(!result.Success)
            {
                return BadRequest("Hệ thống gặp lỗi khi cố truy cập lịch sử giao dịch của bạn.");
            }

            return Ok(result);
        }
    }
}
