using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Helper;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;
using OnDemandTutorApi.DataAccessLayer.Repositories.RepoImpl;
using System.Drawing.Printing;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl
{
    public class RequestCategoryService : IRequestCategoryService
    {
        private readonly IMapper _mapper;
        private IRequestCategoryRepo _requestCategoryRepo;
        private readonly IRequestRepo _requestRepo;
        private readonly IResponseRepo _responseRepo;

        public static int PAGE_SIZE { get; set; } = 5;

        public RequestCategoryService(IMapper mapper, IRequestCategoryRepo requestCategoryRepo, IRequestRepo requestRepo, IResponseRepo responseRepo)
        {
            _mapper = mapper;
            _requestCategoryRepo = requestCategoryRepo;
            _requestRepo = requestRepo;
            _responseRepo = responseRepo;
        }
        public async Task<ResponseApiDTO> CreateAsync(RequestCategoryDTO requestCategoryDTO)
        {
            var existCategory = await _requestCategoryRepo.GetByNameAsync(requestCategoryDTO.CategoryName);

            if (existCategory != null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Hệ thống đã tồn tại request {requestCategoryDTO.CategoryName}."
                };
            }
            
            var category = _mapper.Map<RequestCategory>(requestCategoryDTO);

            var result = await _requestCategoryRepo.CreateAsync(category);

            if(!result)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Hệ thống gặp lỗ khi thêm một loại request mới."
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = "Thêm một loại request mới thành công."
            };
        }

        public async Task<ResponseApiDTO<IEnumerable<RequestCategoryDTOWithId>>> GetAllAsync(string? search, string? sortBy, int page = 1)
        {
            var categories = await _requestCategoryRepo.GetAllAsync();

            if (!string.IsNullOrEmpty(search))
            {
                categories = categories.Where(x => x.CategoryName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }


            categories = categories.OrderBy(x => x.CategoryName);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "des": categories = categories.OrderByDescending(x => x.CategoryName); break;
                }
            }

            var result = PaginatedList<RequestCategory>.Create(categories, page, PAGE_SIZE);

            if (result.IsNullOrEmpty())
            {
                return new ResponseApiDTO<IEnumerable<RequestCategoryDTOWithId>>
                {
                    Success = true,
                    Message = "Hiện tại chưa có loại request nào được thêm."
                };
            }

            return new ResponseApiDTO<IEnumerable<RequestCategoryDTOWithId>>
            {
                Success = true,
                Message = "Đây là danh sách các loại yêu cầu",
                Data = result.Select(x => new RequestCategoryDTOWithId
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName,
                })
            };
        }

        public async Task<ResponseApiDTO<RequestCategoryDTOWithId>> GetByIdAsync(int id)
        {
            var category = await _requestCategoryRepo.GetByIdAsync(id);

            if (category == null)
            {
                return new ResponseApiDTO<RequestCategoryDTOWithId>
                {
                    Success = false,
                    Message = $"Không tìm thấy loại yêu cầu ứng với Id: {id}."
                };
            }

            return new ResponseApiDTO<RequestCategoryDTOWithId>
            {
                Success = true,
                Message = $"Đây là loại yêu cầu ứng với Id: {id}.",
                Data = new RequestCategoryDTOWithId
                {
                    Id = id,
                    CategoryName = category.CategoryName,
                }
            };
        }

        public async Task<ResponseApiDTO> UpdateAsync(RequestCategoryDTO requestCategoryDTO, int id)
        {
            var category = await _requestCategoryRepo.GetByIdAsync(id);
            if (category == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Hệ thống không tìm thấy loại yêu cầu có Id: {id}."
                };
            }

            var existCategory = await _requestCategoryRepo.GetByNameAsync(requestCategoryDTO.CategoryName);

            if (existCategory != null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Hệ thống đã tồn tại request {requestCategoryDTO.CategoryName}."
                };
            }

            var updateCategory = _mapper.Map(requestCategoryDTO, category);

            var result = await _requestCategoryRepo.UpdateAsync(updateCategory);

            if(!result)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Hệ thống gặp lỗi khi cập nhật loại yêu cầu theo ý bạn."
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = "Cập nhật tên loại yêu cầu thành công."
            };
        }

        public async Task<ResponseApiDTO> DeleteAsync(int id)
        {
            var category = await _requestCategoryRepo.GetByIdAsync(id);

            if (category == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Hệ thống không tìm thấy loại yêu cầu có Id: {id}."
                };
            }

            var requests = await _requestRepo.GetAllAsync();
            var responses = await _responseRepo.GetAllAsync();
            var requestsWithCategory = requests.Where(x => x.RequestCategoryId == id).ToList();
            var responsesToDelete = new List<Response>();

            foreach (var request in requestsWithCategory)
            {
                var responsesForRequest = responses.Where(x => x.RequestId == request.Id).ToList();
                responsesToDelete.AddRange(responsesForRequest); // Thêm tất cả các phản hồi vào danh sách responsesToDelete
            }

            // Xóa các phản hồi trước
            foreach (var response in responsesToDelete)
            {
                await _responseRepo.DeleteAsync(response);
            }

            if(responsesToDelete.Any())
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Lỗi xảy ra khi xóa các phản hồi liên quan đến loại yêu cầu."
                };
            }

            // Xóa các yêu cầu sau khi xóa hết các phản hồi
            foreach (var request in requestsWithCategory)
            {
                await _requestRepo.DeleteAsync(request);
            }

            if (requestsWithCategory.Any())
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Lỗi xảy ra khi xóa các yêu cầu liên quan đến loại yêu cầu."
                };
            }

            var result = await _requestCategoryRepo.DeleteAsync(category);

            if(!result)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Lỗi xảy ra khi xóa loại yêu cầu."
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = "Đã xóa loại yêu cầu thành công."
            };

        }
    }
}
