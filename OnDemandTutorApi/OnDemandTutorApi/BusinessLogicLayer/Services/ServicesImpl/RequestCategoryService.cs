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

        public static int PAGE_SIZE { get; set; } = 5;

        public RequestCategoryService(IMapper mapper, IRequestCategoryRepo requestCategoryRepo)
        {
            _mapper = mapper;
            _requestCategoryRepo = requestCategoryRepo;
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

            var result = await _requestCategoryRepo.DeleteAsync(category);

            if (!result)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Hệ thống gặp lỗi khi xóa loại yêu cầu theo ý bạn."
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = "Xóa loại yêu cầu thành công."
            };
        }
    }
}
