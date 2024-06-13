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

            var check = await CheckValidTime(timeRequestDTO, subjectLevel.TutorId);
            if(!check.Success)
            {
                return new ResponseApiDTO<TimeResponseDTO>
                {
                    Success = false,
                    Message = check.Message
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
                    StartSlot = time.StartSlot.TimeOfDay.ToString(@"hh\:mm\:ss"),
                    EndSlot = time.EndSlot.TimeOfDay.ToString(@"hh\:mm\:ss"),
                    Date = time.Date.Date.ToString("dd/MM/yyyy"),
                }
            };
        }

        public async Task<ResponseApiDTO<IEnumerable<TimeResponseDTO>>> GetAllForStudentAsync(string userId, string? timeId, string? sortBy, DateTime? from, DateTime? to, int page = 1)
        {
            var times =  await _timeRepo.GetAllByStudentIdAsync(userId);

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
                    StartSlot = x.StartSlot.TimeOfDay.ToString(@"hh\:mm\:ss"),
                    EndSlot = x.EndSlot.TimeOfDay.ToString(@"hh\:mm\:ss"),
                    Date = x.Date.Date.ToString("dd/MM/yyyy"),
                })
            };
        }

        public async Task<ResponseApiDTO> CheckValidTime(TimeRequestDTO timeRequestDTO, int tutorId)
        {
            if(timeRequestDTO.StartSlot.Date != timeRequestDTO.EndSlot.Date ||
                timeRequestDTO.StartSlot.Date != timeRequestDTO.Date || timeRequestDTO.EndSlot.Date != timeRequestDTO.Date)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Thời gian bắt đầu và thời gian kết thúc buổi học phải cùng một ngày."
                };
            }

            // Kiểm tra không được đặt lịch vào thời gian ở quá khứ
            DateTime currentDateTime = DateTime.Now;
            TimeSpan currentTime = currentDateTime.TimeOfDay;

            if (timeRequestDTO.Date < currentDateTime.Date ||
                (timeRequestDTO.Date == currentDateTime.Date && timeRequestDTO.StartSlot.TimeOfDay < currentTime))
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Bạn không thể đặt lịch vào thời gian ở quá khứ!!!"
                };
            }

            var times = await _timeRepo.GetAllByTutorIdAsync(tutorId);

            // Danh sách các lớp trong cùng một ngày
            var timesInSameDay = times.Where(t => t.Date == timeRequestDTO.Date).ToList();

            // Kiểm tra số lượng lớp trong ngày
            if (timesInSameDay.Count >= 5)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Bạn không thể đặt lịch dạy quá 5 buổi trong cùng một ngày!!!"
                };
            }

            // Kiểm tra trùng giờ
            foreach (var time in timesInSameDay)
            {
                if ((timeRequestDTO.StartSlot.TimeOfDay - time.EndSlot.TimeOfDay).TotalHours < 1)
                {
                    return new ResponseApiDTO
                    {
                        Success = false,
                        Message = "Các lịch học phải cách nhau ít nhất 1 tiếng"
                    };
                }
            }

            // Kiểm tra khoảng cách giữa StartSlot và EndSlot ít nhất 2 giờ
            if ((timeRequestDTO.EndSlot - timeRequestDTO.StartSlot).TotalHours < 2)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Thời gian giữa StartSlot và EndSlot phải cách nhau ít nhất 2 giờ!!!"
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
            };
        }
    }
}
