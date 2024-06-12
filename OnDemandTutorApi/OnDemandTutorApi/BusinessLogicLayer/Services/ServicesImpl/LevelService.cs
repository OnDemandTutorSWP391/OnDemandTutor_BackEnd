using AutoMapper;
using Mailjet.Client.Resources;
using Microsoft.IdentityModel.Tokens;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Helper;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;
using OnDemandTutorApi.DataAccessLayer.Repositories.RepoImpl;
using Org.BouncyCastle.Asn1.Ocsp;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl
{
    public class LevelService : ILevelService
    {
        private readonly ILevelRepo _levelRepo;
        private readonly IMapper _mapper;

        public static int PAGE_SIZE { get; set; } = 5;

        public LevelService(ILevelRepo levelRepo, IMapper mapper)
        {
            _levelRepo = levelRepo;
            _mapper = mapper;
        }
        public async Task<ResponseApiDTO> CreateAsync(LevelDTO levelDTO)
        {
            var existLevel = await _levelRepo.GetByNameAsync(levelDTO.Name);

            if (existLevel != null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Hệ thống đã tồn tại cấp bậc {levelDTO.Name}"
                };
            }

            var level = _mapper.Map<Level>(levelDTO);
            
            var result  = await _levelRepo.CreateAsync(level);

            if(!result)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Hệ thống gặp lỗi khi thêm cấp bậc môn học"
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = "Thêm cấp bậc môn học thành công."
            };
        }

        public async Task<ResponseApiDTO<IEnumerable<LevelDTOWithId>>> GetAllAsync(string? search, string? levelId, string? sortBy, int page = 1)
        {
            var levels = await _levelRepo.GetAllAsync();

            if (!string.IsNullOrEmpty(search))
            {
                levels = levels.Where(x => x.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (!string.IsNullOrEmpty(levelId))
            {
                levels = levels.Where(x => x.Id == Convert.ToInt32(levelId));
            }

            levels = levels.OrderBy(x => x.Name);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "des": levels = levels.OrderByDescending(x => x.Name); break;
                }
            }

            var result = PaginatedList<Level>.Create(levels, page, PAGE_SIZE);

            if (result.IsNullOrEmpty())
            {
                return new ResponseApiDTO<IEnumerable<LevelDTOWithId>>
                {
                    Success = true,
                    Message = "Hiện tại chưa có cấp bậc nào được thêm."
                };
            }

            return new ResponseApiDTO<IEnumerable<LevelDTOWithId>>
            {
                Success = true,
                Message = "Đây là danh sách các cấp bậc của hệ thống",
                Data = result.Select(x => new LevelDTOWithId
                {
                    Id = x.Id,
                    Name = x.Name,
                })
            };
        }

        public async Task<ResponseApiDTO> UpdateAsync(int id, LevelDTO levelDTO)
        {
            var level = await _levelRepo.GetByIdAsync(id);

            if(level == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Hệ thống không tồn tại cấp bậc với Id: {id}."
                };
            }

            var existLevel = await _levelRepo.GetByNameAsync(levelDTO.Name);

            if (existLevel != null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Hệ thống đã tồn tại cấp bậc {levelDTO.Name}"
                };
            }

            var updateLevel = _mapper.Map(levelDTO, level);

            var result =  await _levelRepo.UpdateAsync(updateLevel);

            if(!result)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Hệ thống gặp lỗi khi cập nhật cấp bậc với Id: {id}."
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = $"Hệ thống cập nhật cấp bậc với Id: {id} thành công."
            };
        }
    }
}
