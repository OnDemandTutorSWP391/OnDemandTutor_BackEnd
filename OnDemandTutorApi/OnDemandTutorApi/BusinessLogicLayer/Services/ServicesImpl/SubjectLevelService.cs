using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Helper;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;
using OnDemandTutorApi.DataAccessLayer.Repositories.RepoImpl;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl
{
    public class SubjectLevelService : ISubjectLevelService
    {
        private readonly ISubjectLevelRepo _subjectLevelRepo;
        private readonly ITutorRepo _tutorRepo;
        private readonly ILevelRepo _levelRepo;
        private readonly ISubjectRepo _subjectRepo;
        private readonly IMapper _mapper;

        public static int PAGE_SIZE { get; set; } = 5;

        public SubjectLevelService(ISubjectLevelRepo subjectLevelRepo, ITutorRepo tutorRepo, ILevelRepo levelRepo, 
            ISubjectRepo subjectRepo, IMapper mapper)
        {
            _subjectLevelRepo = subjectLevelRepo;
            _tutorRepo = tutorRepo;
            _levelRepo = levelRepo;
            _subjectRepo = subjectRepo;
            _mapper = mapper;
        }
        public async Task<ResponseApiDTO<SubjectLevelDTOWithId>> CreateAsync(string userId, SubjectLevelDTO subjectLevelDTO)
        {
            var tutor = await _tutorRepo.GetTutorByUserIdAsync(userId);

            if(!tutor.Status.Equals("Chấp thuận"))
            {
                return new ResponseApiDTO<SubjectLevelDTOWithId>
                {
                    Success = false,
                    Message = "Bạn chưa được cấp phép giảng dạy ở hệ thống của chúng tôi."
                };
            }

            var level = await _levelRepo.GetByNameAsync(subjectLevelDTO.LevelName);

            if(level == null)
            {
                return new ResponseApiDTO<SubjectLevelDTOWithId>
                {
                    Success = false,
                    Message = $"Cấp bậc {subjectLevelDTO.LevelName} không tồn tại trong hệ thống."
                };
            }

            var subject = await _subjectRepo.GetByNameAsync(subjectLevelDTO.SubjectName);
            if (subject == null)
            {
                return new ResponseApiDTO<SubjectLevelDTOWithId>
                {
                    Success = false,
                    Message = $"Môn học {subjectLevelDTO.SubjectName} không tồn tại trong hệ thống."
                };
            }

            var subjectLevel = _mapper.Map<SubjectLevel>(subjectLevelDTO);
            subjectLevel.TutorId = tutor.TutorId;
            subjectLevel.LevelId = level.Id;
            subjectLevel.SubjectId = subject.Id;

            var reuslt = await _subjectLevelRepo.CreateAsync(subjectLevel);

            if(!reuslt)
            {
                return new ResponseApiDTO<SubjectLevelDTOWithId>
                {
                    Success = false,
                    Message = "Hệ thống đã gặp lỗi trong quá trình đăng kí môn giảng dạy."
                };
            }

            return new ResponseApiDTO<SubjectLevelDTOWithId>
            {
                Success = true,
                Message = "Bạn đã đăng kí thành công. Đây là thông tin môn đăng kí của bạn.",
                Data = new SubjectLevelDTOWithId
                {
                    Id = subjectLevel.Id,
                    LevelName = level.Name,
                    SubjectName = subject.Name,
                    Description = subjectLevel.Description
                }
            };
        }

        public async Task<ResponseApiDTO<IEnumerable<SubjectLevelDTOWithData>>> GetAllAsync(string? level, string? subject, string? tutor, int page = 1)
        {
            var subjectLevels = await _subjectLevelRepo.GetAllAsync();

            if(!string.IsNullOrEmpty(level))
            {
                subjectLevels = subjectLevels.Where(x => x.Level.Name == level);
            }

            if (!string.IsNullOrEmpty(subject))
            {
                subjectLevels = subjectLevels.Where(x => x.Subject.Name == subject);
            }

            if (!string.IsNullOrEmpty(tutor))
            {
                subjectLevels = subjectLevels.Where(x => x.Tutor.User.FullName == tutor);
            }

            var result = PaginatedList<SubjectLevel>.Create(subjectLevels, page, PAGE_SIZE);

            if (result.IsNullOrEmpty())
            {
                return new ResponseApiDTO<IEnumerable<SubjectLevelDTOWithData>>
                {
                    Success = true,
                    Message = "Hiện tại chưa có dịch vụ môn học nào được thêm."
                };
            }

            return new ResponseApiDTO<IEnumerable<SubjectLevelDTOWithData>>
            {
                Success = true,
                Message = "Đây là danh sách các môn học của hệ thống",
                Data = result.Select(x => new SubjectLevelDTOWithData
                {
                    Id = x.Id,
                    LevelName = x.Level.Name,
                    SubjectName = x.Subject.Name,
                    TutorName = x.Tutor.User.FullName,
                    Description = x.Description,
                })
            };
        }

        public async Task<ResponseApiDTO> UpdateAsync(int id, SubjectLevelDTO subjectLevelDTO)
        {
            var subjectLevel = await _subjectLevelRepo.GetByIdAsync(id);

            if(subjectLevel == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Không tồn tại môn học của bạn với mã {id}."
                };
            }

            var update = _mapper.Map(subjectLevelDTO, subjectLevel);

            var result = await _subjectLevelRepo.UpdateAsync(update);

            if(!result)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Hệ thống gặp lỗi khi cập nhật môn học của bạn."
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = "Cập nhật thông tin môn học của bạn thành công."
            };
        }

        public async Task<ResponseApiDTO> DeleteAsync(int id)
        {
            var subjectLevel = await _subjectLevelRepo.GetByIdAsync(id);

            if (subjectLevel == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Không tồn tại môn học của bạn với mã {id}."
                };
            }

            var result = await _subjectLevelRepo.DeleteAsync(subjectLevel);

            if (!result)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Hệ thống gặp lỗi khi xóa môn học của bạn."
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = "Xóa môn học của bạn thành công."
            };
        }
    }
}
