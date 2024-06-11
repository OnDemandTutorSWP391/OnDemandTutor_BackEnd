using AutoMapper;
using Humanizer;
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
    public class TimeService : ITimeService
    {
        private readonly IMapper _mapper;
        private readonly ITimeRepo _timeRepo;
        private readonly ISubjectLevelRepo _subjectLevelRepo;
        private readonly IUserRepo _userRepo;
        private readonly IEmailService _emailService;

        public static int PAGE_SIZE { get; set; } = 5;

        public TimeService(IMapper mapper, ITimeRepo timeRepo, ISubjectLevelRepo subjectLevelRepo, IUserRepo userRepo, IEmailService emailService)
        {
            _mapper = mapper;
            _timeRepo = timeRepo;
            _subjectLevelRepo = subjectLevelRepo;
            _userRepo = userRepo;
            _emailService = emailService;
        }

        public ResponseApiDTO CheckValidTime(Time time)
        {
            //check 1:
            if (time.StartSlot < DateTime.Now || time.EndSlot < DateTime.Now || time.Date < DateTime.Now)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = ""
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = ""
            };
        }

        public async Task<ResponseApiDTO<TimeResponseDTO>> CreateAsync(TimeRequestDTO timeRequestDTO)
        {
            var subjectLevel = await _subjectLevelRepo.GetByIdAsync(timeRequestDTO.SubjectLevelId);

            if(subjectLevel == null)
            {
                return new ResponseApiDTO<TimeResponseDTO>
                {
                    Success = false,
                    Message = $"Không tồn tại khóa học với Id: {timeRequestDTO.SubjectLevelId}."
                };
            }

            var time = _mapper.Map<Time>(timeRequestDTO);

            var existTime = await _timeRepo.GetByDateAsync(time.StartSlot, time.EndSlot, time.Date);
            if(existTime != null)
            {
                return new ResponseApiDTO<TimeResponseDTO>
                {
                    Success = false,
                    Message = $"Lịch học đã trùng với lịch có Id: {existTime.Id}."
                };
            }

            var result = await _timeRepo.CreateAsync(time);

            if(!result)
            {
                return new ResponseApiDTO<TimeResponseDTO>
                {
                    Success = false,
                    Message = $"Hệ thống gặp lỗi khi thêm lịch học."
                };
            }

            var studentJoins = subjectLevel.StudentJoins;
            foreach (var item in studentJoins)
            {
                var student = await _userRepo.GetByIdAsync(item.UserId);
                var title = $"Thư thông báo về khóa học {time.SubjectLevelId}!";
                var content = @$"- Giảng viên chủ nhiệm khóa học đã cập nhật lịch học của khóa.
                             - Sử dụng Id: {time.Id} vào mục Time để tra cứu thông tin chi tiết.
                             - Vui lòng thường xuyên kiểm tra Email bằng tài khoản này để cập nhật thông tin lớp học.";
                var message = new EmailDTO
                (
                    new string[] { student.Email! },
                        title,
                        content!
                );
                _emailService.SendEmail(message);
            }

            return new ResponseApiDTO<TimeResponseDTO>
            {
                Success = true,
                Message = "Thêm lịch học cho khóa thành công.",
                Data = new TimeResponseDTO
                {
                    Id = time.Id,
                    SubjectLevelId = time.SubjectLevelId,
                    SlotName = time.SlotName,
                    StartSlot = time.StartSlot,
                    EndSlot = time.EndSlot,
                    Date = time.Date,
                }
            };
        }

        public async Task<ResponseApiDTO<IEnumerable<TimeResponseDTO>>> GetAllForStudentAsync(string userId, string? timeId, string? sortBy, DateTime? from, DateTime? to, int page = 1)
        {
            var times =  await _timeRepo.GetAllByUserIdAsync(userId);

            if(!string.IsNullOrEmpty(timeId))
            {
                times = times.Where(t => t.Id == Convert.ToInt64(timeId));
            }

            if(from.HasValue)
            {
                times = times.Where(t => t.Date >= from.Value);
            }

            if (to.HasValue)
            {
                times = times.Where(t => t.Date <= to.Value);
            }

            times = times.OrderBy(t => t.Date);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "des": times = times.OrderByDescending(x => x.Date); break;
                }
            }

            var result = PaginatedList<Time>.Create(times, page, PAGE_SIZE);

            if (result.IsNullOrEmpty())
            {
                return new ResponseApiDTO<IEnumerable<TimeResponseDTO>>
                {
                    Success = true,
                    Message = "Hiện tại gia sư chưa thêm lịch cho môn học này."
                };
            }

            return new ResponseApiDTO<IEnumerable<TimeResponseDTO>>
            {
                Success = true,
                Message = "Đây là danh sách các lịch học của bạn",
                Data = result.Select(x => new TimeResponseDTO
                {
                    Id = x.Id,
                    SubjectLevelId = x.SubjectLevelId,
                    SlotName = x.SlotName,
                    StartSlot = x.StartSlot,
                    EndSlot = x.EndSlot,
                    Date = x.Date,
                })
            };
        }
    }
}
