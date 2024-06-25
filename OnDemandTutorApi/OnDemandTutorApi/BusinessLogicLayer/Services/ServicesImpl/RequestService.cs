using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Helper;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.DataAccessLayer.DAO;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;
using OnDemandTutorApi.DataAccessLayer.Repositories.RepoImpl;
using System.Drawing.Printing;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepo _requestRepo;
        private readonly IUserRepo _userRepo;
        private readonly IRequestCategoryRepo _requestCategoryRepo;
        private readonly IMapper _mapper;
        private readonly IResponseRepo _responseRepo;

        public static int PAGE_SIZE { get; set; } = 5;

        public RequestService(IRequestRepo requestRepo, IUserRepo userRepo, IRequestCategoryRepo requestCategoryRepo, IMapper mapper, IResponseRepo responseRepo)
        {
            _requestRepo = requestRepo;
            _userRepo = userRepo;
            _requestCategoryRepo = requestCategoryRepo;
            _mapper = mapper;
            _responseRepo = responseRepo;
        }
        public async Task<ResponseApiDTO> CreateAsync(string userId, RequestDTO requestDTO)
        {
            var user = await _userRepo.GetByIdAsync(userId);

            if (user == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Không thể xác thực được người dùng."
                };
            }

            var category = await _requestCategoryRepo.GetByIdAsync(requestDTO.RequestCategoryId);

            if(category == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Loại yêu cầu {category.CategoryName} không tồn tại trong hệ thống."
                };
            }

            var request = _mapper.Map<Request>(requestDTO);
            request.UserId = userId;    

            var result = await _requestRepo.CreateAsync(request);

            if(!result)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Lỗi hệ thống khi gửi yêu cầu."
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = $"Bạn đã gửi yêu cầu thành công."
            };
        }

        public async Task<ResponseApiDTO<IEnumerable<RequestDTOWithUserData>>> UserGetAllAsync(string userId, string? search, DateTime? from, DateTime? to, string? sortBy, int page = 1)
        {
            var userRequests = await _requestRepo.GetAllByUserIdAsync(userId);

            if (!string.IsNullOrEmpty(search))
            {
                userRequests = userRequests.Where(x => x.Category.CategoryName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (from.HasValue)
            {
                userRequests = userRequests.Where(x => x.CreatedDate >= from.Value);
            }

            if (to.HasValue)
            {
                userRequests = userRequests.Where(x => x.CreatedDate <= to.Value);
            }

            userRequests = userRequests.OrderBy(x => x.CreatedDate);

            if(!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "des": userRequests = userRequests.OrderByDescending(x => x.CreatedDate); break;
                }
            }

            var result = PaginatedList<Request>.Create(userRequests, page, PAGE_SIZE);

            if (result.IsNullOrEmpty())
            {
                return new ResponseApiDTO<IEnumerable<RequestDTOWithUserData>>
                {
                    Success = true,
                    Message = "Hiện tại chưa có yêu cầu nào được gửi đi."
                };
            }

            return new ResponseApiDTO<IEnumerable<RequestDTOWithUserData>>
            {
                Success = true,
                Message = "Đây là danh sách các yêu cầu của bạn",
                Data = result.Select(x => new RequestDTOWithUserData
                {
                    Id = x.Id,
                    CategoryName = x.Category.CategoryName,
                    FullName = x.User.FullName,
                    CreatedDate = x.CreatedDate,
                    Description = x.Description,
                    Status = x.Status,
                })
            };
        }

        public async Task<ResponseApiDTO<IEnumerable<RequestDTOWithData>>> GetAllAsync(string? search, string? userId, DateTime? from, DateTime? to, string? sortBy, int page = 1)
        {
            var requests = await _requestRepo.GetAllAsync();

            if (!string.IsNullOrEmpty(search))
            {
                requests = requests.Where(x => x.Category.CategoryName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (!string.IsNullOrEmpty(userId))
            {
                requests = requests.Where(x => x.UserId == userId);
            }

            if (from.HasValue)
            {
                requests = requests.Where(x => x.CreatedDate >= from.Value);
            }

            if (to.HasValue)
            {
                requests = requests.Where(x => x.CreatedDate <= to.Value);
            }

            requests = requests.OrderBy(x => x.CreatedDate);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "des": requests = requests.OrderByDescending(x => x.CreatedDate); break;
                }
            }

            var result = PaginatedList<Request>.Create(requests, page, PAGE_SIZE);

            if (result.IsNullOrEmpty())
            {
                return new ResponseApiDTO<IEnumerable<RequestDTOWithData>>
                {
                    Success = true,
                    Message = "Hiện tại chưa có yêu cầu nào được gửi đi."
                };
            }

            return new ResponseApiDTO<IEnumerable<RequestDTOWithData>>
            {
                Success = true,
                Message = "Đây là danh sách các yêu cầu của bạn",
                Data = result.Select(x => new RequestDTOWithData
                {
                    Id = x.Id,
                    CategoryName = x.Category.CategoryName,
                    UserId = x.UserId,
                    FullName = x.User.FullName,
                    CreatedDate = x.CreatedDate,
                    Description = x.Description,
                    Status = x.Status,
                })
            };
        }

        public async Task<ResponseApiDTO> UpdateStatusRequestAsync(int id, RequestUpdateStatusDTO requestUpdate)
        {
            var request = await _requestRepo.GetByIdAsync(id);

            if (request == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Không thể tìm thấy yêu cầu nào với Id: {id}."
                };
            }

            var _request = _mapper.Map(requestUpdate, request);

            var result = await _requestRepo.UpdateAsync(_request);

            if(!result)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Hệ thống gặp lỗi khi cập nhật trạng thái của yêu cầu."
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = "Cập nhật trạng thái của yêu cầu thành công."
            };
        }

        public async Task<ResponseApiDTO> DeleteAsync(int id)
        {
            var request = await _requestRepo.GetByIdAsync(id);
            if(request == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Không tìm thấy yêu cầu trong hệ thống."
                };
            }

            var responses = await _responseRepo.GetAllAsync();
            responses = responses.Where(x => x.RequestId == id);

            foreach (var item in responses)
            {
                await _responseRepo.DeleteAsync(item);
            }

            if(!responses.Any())
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Lỗi xảy ra khi xóa các phản hồi liên quan đến yêu cầu."
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = "Xóa yêu cầu thành công"
            };
        }

        public async Task<ResponseApiDTO> UpdateAsync(int id, RequestDTO requestUpdate)
        {
            var request = await _requestRepo.GetByIdAsync(id);
            if(request == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Không thìm thấy yêu cầu trong hệ thống."
                };
            }

            var update = _mapper.Map<Request>(requestUpdate);
            var result = await _requestRepo.UpdateAsync(update);

            if(!result)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Lỗi xảy ra khi cập nhật yêu cầu trong hệ thống."
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = "Cập nhật yêu cầu thành công."
            };
        }
    }
}
