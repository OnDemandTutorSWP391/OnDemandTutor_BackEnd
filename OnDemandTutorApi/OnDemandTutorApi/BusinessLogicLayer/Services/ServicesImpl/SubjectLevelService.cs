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
        public async Task<ResponseApiDTO<SubjectLevelResponseDTO>> CreateAsync(string userId, SubjectLevelRequestDTO subjectLevelDTO)
        {
            var tutor = await _tutorRepo.GetTutorByUserIdAsync(userId);

            if(!tutor.Status.Equals("Chấp thuận"))
            {
                return new ResponseApiDTO<SubjectLevelResponseDTO>
                {
                    Success = false,
                    Message = "Bạn chưa được cấp phép giảng dạy ở hệ thống của chúng tôi."
                };
            }
            subjectLevelDTO.TutorId = tutor.Id;

            var level = await _levelRepo.GetByIdAsync(subjectLevelDTO.LevelId);

            if(level == null)
            {
                return new ResponseApiDTO<SubjectLevelResponseDTO>
                {
                    Success = false,
                    Message = $"Cấp bậc {subjectLevelDTO.LevelId} không tồn tại trong hệ thống."
                };
            }

            var subject = await _subjectRepo.GetByIdAsync(subjectLevelDTO.SubjectId);
            if (subject == null)
            {
                return new ResponseApiDTO<SubjectLevelResponseDTO>
                {
                    Success = false,
                    Message = $"Môn học {subjectLevelDTO.SubjectId} không tồn tại trong hệ thống."
                };
            }

            var subjectLevel = _mapper.Map<SubjectLevel>(subjectLevelDTO);

            var reuslt = await _subjectLevelRepo.CreateAsync(subjectLevel);

            if(!reuslt)
            {
                return new ResponseApiDTO<SubjectLevelResponseDTO>
                {
                    Success = false,
                    Message = "Hệ thống đã gặp lỗi trong quá trình đăng kí môn giảng dạy."
                };
            }

            return new ResponseApiDTO<SubjectLevelResponseDTO>
            {
                Success = true,
                Message = "Bạn đã đăng kí thành công. Đây là thông tin môn đăng kí của bạn.",
                Data = new SubjectLevelResponseDTO
                {
                    Id = subjectLevel.Id,
                    LevelName = level.Name,
                    SubjectName = subject.Name,
                    TutorName = tutor.User.FullName,
                    Description = subjectLevel.Description,
                    Url = subjectLevel.Url,
                    Coin = subjectLevel.Coin,
                    LimitMember = $"{subjectLevel.LimitMember}",
                }
            };
        }

        public async Task<ResponseApiDTO<IEnumerable<SubjectLevelResponseDTO>>> GetAllAsync(string? level, string? subject, string? tutor, int page = 1)
        {
            var subjectLevels = await _subjectLevelRepo.GetAllAsync();

            if (!string.IsNullOrEmpty(level))
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
                return new ResponseApiDTO<IEnumerable<SubjectLevelResponseDTO>>
                {
                    Success = true,
                    Message = "Hiện tại chưa có dịch vụ môn học nào được thêm."
                };
            }

            var subjectLevelResponses = new List<SubjectLevelResponseDTO>();
            foreach (var subjectLevel in result)
            {
                var studentJoins = subjectLevel.StudentJoins;
                var count = studentJoins.Count();
                var subjectLevelResponse = _mapper.Map<SubjectLevelResponseDTO>(subjectLevel);
                subjectLevelResponse.LevelName = subjectLevel.Level.Name;
                subjectLevelResponse.SubjectName = subjectLevel.Subject.Name;
                subjectLevelResponse.TutorName = subjectLevel.Tutor.User.FullName;
                subjectLevelResponse.LimitMember = $"{count}/{subjectLevel.LimitMember}";
                subjectLevelResponses.Add(subjectLevelResponse);
            }

            return new ResponseApiDTO<IEnumerable<SubjectLevelResponseDTO>>
            {
                Success = true,
                Message = "Đây là danh sách các môn học của hệ thống",
                Data = subjectLevelResponses
            };
        }

        public async Task<ResponseApiDTO> UpdateAsync(int id, string userId, SubjectLevelRequestDTO subjectLevelDTO)
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

            if(subjectLevel.Tutor.User.Id != userId)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Bạn không thể cập nhật thông tin khóa học của giảng viên khác."
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

    }
}
