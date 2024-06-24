using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Helper;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.DataAccessLayer.DAO;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;
using OnDemandTutorApi.DataAccessLayer.Repositories.RepoImpl;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl
{
    public class CoinManagementService : ICoinManagementService
    {
        private readonly ICoinManagementRepo _coinManagementRepo;
        private readonly IUserRepo _userRepo;   
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public static int PAGE_SIZE { get; set; } = 5;

        public CoinManagementService(ICoinManagementRepo coinManagementRepo, IMapper mapper, IUserRepo userRepo, IEmailService emailService)
        {
            _coinManagementRepo = coinManagementRepo;
            _userRepo = userRepo;
            _mapper = mapper;
            _emailService = emailService;
        }

        //DEPOSIT
        public async Task<ResponseApiDTO<CoinResponseDTO>> DepositAsync(CoinDTO coinRequest)
        {
            var coinRecord = _mapper.Map<CoinManagement>(coinRequest);

            var user = await _userRepo.GetByIdAsync(coinRecord.UserId);

            var existCoinRecords = await _coinManagementRepo.GetAllAsync();
            if(existCoinRecords.FirstOrDefault(x => x.TransactionId == coinRequest.TransactionId) != null)
            {
                return new ResponseApiDTO<CoinResponseDTO>
                {
                    Success = false,
                    Message = "Hệ thống gặp lỗi khi lưu lại giao dịch của người dùng"
                };
            }

            var result = await _coinManagementRepo.CreateCoinRecord(coinRecord);

            if(!result)
            {
                return new ResponseApiDTO<CoinResponseDTO>
                {
                    Success = false,
                    Message = "Hệ thống gặp lỗi khi lưu lại giao dịch của người dùng"
                };
            }

            return new ResponseApiDTO<CoinResponseDTO>
            {
                Success = true,
                Message = "Lưu giao dịch thành công.",
                Data = new CoinResponseDTO
                {
                    Coin = coinRecord.Coin,
                    Date = coinRecord.Date,
                    FullName = user.FullName
                }
            }; 
        }


        //GET TOTAL COIN
        public async Task<ResponseApiDTO<float>> GetTotalCoinForUserAsync(string userId)
        {
            var result = await _coinManagementRepo.GetTotalCoinForUserAsync(userId);

            if(result == 0)
            {
                return new ResponseApiDTO<float>
                {
                    Success = true,
                    Message = "Bạn hiện đang có 0 coin trong tài khoản của mình. Hãy nạp thêm coin vào để trải nghiệm dịch vụ",
                    Data = result
                };
            }

            return new ResponseApiDTO<float>
            {
                Success = true,
                Message = "Đây là số dư hiện tại của bạn",
                Data = result
            };
        }


        //GET COIN RECORD BY USER ID
        public async Task<ResponseApiDTO<IEnumerable<CoinResponseDTO>>> GetTransactionForUserAsync(string userId, DateTime? from, DateTime? to, string? sortBy, int page = 1)
        {
            var records = await _coinManagementRepo.GetByUserIdAsync(userId);

            if(from.HasValue)
            {
                records = records.Where(x => x.Date >= from.Value);
            }

            if (to.HasValue)
            {
                records = records.Where(x => x.Date <= to.Value );
            }

            records = records.OrderBy(x => x.Date);

            if(!string.IsNullOrEmpty(sortBy))
            {
                switch(sortBy)
                {
                    case "des": records = records.OrderByDescending(x => x.Date); break;
                }
            }

            var result = PaginatedList<CoinManagement>.Create(records, page, PAGE_SIZE);

            if (result.IsNullOrEmpty())
            {
                return new ResponseApiDTO<IEnumerable<CoinResponseDTO>>
                {
                    Success = true,
                    Message = "Hiện tại bạn chưa thực hiện giao dịch nào."
                };
            }

            return new ResponseApiDTO<IEnumerable<CoinResponseDTO>>
            {
                Success = true,
                Message = "Đây là danh sách các giao dịch của bạn.",
                Data = result.Select(x => new CoinResponseDTO
                {
                    FullName = x.User.FullName,
                    Coin = x.Coin,
                    Date = x.Date,
                }).ToList()
            };
        }

        //TRANSFER
        public async Task<ResponseApiDTO<CoinTransferResponseDTO>> TransferAsync(string userId, string receiverId, float coin)
        {
            var sender = await _userRepo.GetByIdAsync(userId);

            var receiver = await _userRepo.GetByIdAsync(receiverId);

            if(receiver == null)
            {
                return new ResponseApiDTO<CoinTransferResponseDTO>
                {
                    Success = false,
                    Message = "Người dùng không tồn tại trong hệ thống."
                };
            }

            var senderCoin = _mapper.Map<CoinManagement>(new CoinDTO { UserId = userId, Coin = -coin });
            var send = await _coinManagementRepo.CreateCoinRecord(senderCoin);
            if(!send)
            {
                return new ResponseApiDTO<CoinTransferResponseDTO>
                {
                    Success = false,
                    Message = "Hệ thống gặp lỗi trong quá trình chuyển coin."
                };
            }

            var titleSender = $"Thư xác nhận chuyển coin thành công!";
            var contentSender = @$"- Hệ thống đã xác nhận chuyển coin thành công.
                             - Người nhận - Id: {receiverId}
                             - Số coin: {coin}
                             - Thời gian chuyển: {senderCoin.Date}";
            var messageSender = new EmailDTO
            (
                new string[] { sender.Email! },
                    titleSender,
                    contentSender!
            );
            _emailService.SendEmail(messageSender);

            var receiverCoin = _mapper.Map<CoinManagement>(new CoinDTO { UserId = receiverId, Coin = coin });
            var receive = await _coinManagementRepo.CreateCoinRecord(receiverCoin);
            if(!receive)
            {
                return new ResponseApiDTO<CoinTransferResponseDTO>
                {
                    Success = false,
                    Message = "Hệ thống gặp lỗi trong quá trình chuyển coin. Người dùng chưa thể nhận được coin từ bạn."
                };
            }

            var titleReceiver = $"Thư xác nhận chuyển coin thành công!";
            var contentReceiver = @$"- Hệ thống đã xác nhận chuyển coin thành công.
                             - Người chuyển - Id: {userId}
                             - Số coin: {coin}
                             - Thời gian nhận: {receiverCoin.Date}";
            var messageReceiver = new EmailDTO
            (
                new string[] { receiver.Email! },
                    titleReceiver,
                    contentReceiver!
            );
            _emailService.SendEmail(messageReceiver);

            return new ResponseApiDTO<CoinTransferResponseDTO>
            {
                Success = true,
                Message = "Chuyển coin thành công.",
                Data = new CoinTransferResponseDTO
                {
                    Id = senderCoin.Id,
                    SenderId = userId,
                    SenderName = sender.FullName,
                    ReceiverId = receiverId,
                    ReceiverName = receiver.FullName,
                    Coin = coin,
                    Date = receiverCoin.Date,
                }
            };
        }
    }
}
