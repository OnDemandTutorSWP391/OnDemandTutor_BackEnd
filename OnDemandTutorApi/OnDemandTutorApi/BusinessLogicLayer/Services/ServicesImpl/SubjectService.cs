using AutoMapper;
using Microsoft.IdentityModel.Tokens;
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
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepo _subjectRepo;
        private readonly IMapper _mapper;

        public static int PAGE_SIZE { get; set; } = 5;

        public SubjectService(ISubjectRepo subjectRepo, IMapper mapper)
        {
            _subjectRepo = subjectRepo;
            _mapper = mapper;
        }
        public async Task<ResponseApiDTO> CreateAsync(SubjectDTO subjectDTO)
        {
            var existSubject = await _subjectRepo.GetByNameAsync(subjectDTO.Name);

            if (existSubject != null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Hệ thống đã tồn tại cấp bậc {subjectDTO.Name}"
                };
            }

            var subject = _mapper.Map<Subject>(subjectDTO);

            var result = await _subjectRepo.CreateAsync(subject);

            if(!result)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Hệ thống gặp lỗi khi thêm môn học."
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = "Thêm môn học thành công."
            };
        }

        public async Task<ResponseApiDTO<IEnumerable<SubjectDTOWithId>>> GetAllAsync(string? search, string? subjectId, string? sortBy, int page = 1)
        {
            var subjects = await _subjectRepo.GetAllAsync();

            if (!string.IsNullOrEmpty(search))
            {
                subjects = subjects.Where(x => x.Name == search);
            }

            if (!string.IsNullOrEmpty(subjectId))
            {
                subjects = subjects.Where(x => x.Id == Convert.ToInt32(subjectId));
            }

            subjects = subjects.OrderBy(x => x.Name);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "des": subjects = subjects.OrderByDescending(x => x.Name); break;
                }
            }

            var result = PaginatedList<Subject>.Create(subjects, page, PAGE_SIZE);

            if (result.IsNullOrEmpty())
            {
                return new ResponseApiDTO<IEnumerable<SubjectDTOWithId>>
                {
                    Success = true,
                    Message = "Hiện tại chưa có môn học nào được thêm."
                };
            }

            return new ResponseApiDTO<IEnumerable<SubjectDTOWithId>>
            {
                Success = true,
                Message = "Đây là danh sách các môn học của hệ thống",
                Data = result.Select(x => new SubjectDTOWithId
                {
                    Id = x.Id,
                    Name = x.Name,
                })
            };
        }

        public async Task<ResponseApiDTO> UpdateAsync(int id, SubjectDTO subjectDTO)
        {
            var subject = await _subjectRepo.GetByIdAsync(id);

            if (subject == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Hệ thống không tồn tại cấp bậc với Id: {id}."
                };
            }

            var existSubject = await _subjectRepo.GetByNameAsync(subjectDTO.Name);

            if (existSubject != null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Hệ thống đã tồn tại cấp bậc {subjectDTO.Name}"
                };
            }

            var updateSubject = _mapper.Map(subjectDTO, subject);

            var result = await _subjectRepo.UpdateAsync(updateSubject);

            if (!result)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Hệ thống gặp lỗi khi cập nhật môn học với Id: {id}."
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = $"Hệ thống cập nhật môn học với Id: {id} thành công."
            };
        }
    }
}
