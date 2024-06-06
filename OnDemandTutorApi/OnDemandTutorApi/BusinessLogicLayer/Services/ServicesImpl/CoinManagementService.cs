using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Helper;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
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

        public static int PAGE_SIZE { get; set; } = 5;

        public CoinManagementService(ICoinManagementRepo coinManagementRepo, IMapper mapper, IUserRepo userRepo)
        {
            _coinManagementRepo = coinManagementRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        //DEPOSIT
        public async Task<ResponseApiDTO<CoinResponseDTO>> DepositAsync(CoinDTO coinRequest)
        {
            var coinRecord = _mapper.Map<CoinManagement>(coinRequest);

            var user = await _userRepo.GetByIdAsync(coinRecord.UserId);

            await _coinManagementRepo.CreateCoinRecord(coinRecord);

            return new ResponseApiDTO<CoinResponseDTO>
            {
                Success = true,
                Message = "Deposit successfully",
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
    }
}
