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
        private readonly ITutorRepo _tutorRepo;

        public static int PAGE_SIZE { get; set; } = 5;

        public TimeService(IMapper mapper, ITimeRepo timeRepo, ISubjectLevelRepo subjectLevelRepo, IUserRepo userRepo, IEmailService emailService, ITutorRepo tutorRepo)
        {
            _mapper = mapper;
            _timeRepo = timeRepo;
            _subjectLevelRepo = subjectLevelRepo;
            _userRepo = userRepo;
            _emailService = emailService;
            _tutorRepo = tutorRepo;
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
                var content = $@"
<p>- Giảng viên chủ nhiệm khóa học đã cập nhật lịch học của khóa.</p>
<p>- Sử dụng Id: <strong>{time.Id}</strong> vào mục Time để tra cứu thông tin chi tiết.</p>
<p>- Vui lòng thường xuyên kiểm tra Email bằng tài khoản này để cập nhật thông tin lớp học.</p>";

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

        public async Task<ResponseApiDTO<IEnumerable<TimeResponseDTO>>> GetAllForStudentAsync(string userId, string? timeId, string? subjectLevelId, string? sortBy, DateTime? from, DateTime? to, int page = 1)
        {
            var times =  await _timeRepo.GetAllByStudentIdAsync(userId);
            times = times.Where(x => x.SubjectLevel.Tutor.User.IsLocked == false);

            if(!string.IsNullOrEmpty(timeId))
            {
                times = times.Where(t => t.Id == Convert.ToInt32(timeId));
            }

            if (!string.IsNullOrEmpty(subjectLevelId))
            {
                times = times.Where(t => t.Id == Convert.ToInt32(subjectLevelId));
            }

            if (from.HasValue)
            {
                times = times.Where(t => t.Date.Date >= from.Value);
            }

            if (to.HasValue)
            {
                times = times.Where(t => t.Date.Date <= to.Value);
            }

            times = times.OrderBy(t => t.Date.Date);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "des": times = times.OrderByDescending(x => x.Date.Date); break;
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
                    IsLocked = x.SubjectLevel.Tutor.User.IsLocked,
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
            if (timesInSameDay.Count > 5)
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
                if ((time.StartSlot.TimeOfDay <= timeRequestDTO.EndSlot.TimeOfDay && timeRequestDTO.StartSlot.TimeOfDay <= time.EndSlot.TimeOfDay) ||
                   (timeRequestDTO.StartSlot.TimeOfDay <= time.EndSlot.TimeOfDay && time.StartSlot.TimeOfDay <= timeRequestDTO.EndSlot.TimeOfDay))
                {
                    return new ResponseApiDTO
                    {
                        Success = false,
                        Message = $"Bạn không thể đặt lịch dạy trùng giờ nhau trong cùng một ngày!!!. Lịch đã trùng với lịch có Id: {time.Id}"
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

        public async Task<ResponseApiDTO<IEnumerable<TimeResponseDTO>>> GetAllForTutorAsync(string userId, string? timeId, string? subjectLevelId, string? sortBy, DateTime? from, DateTime? to, int page = 1)
        {
            var tutor = await _tutorRepo.GetTutorByUserIdAsync(userId);
            var times = await _timeRepo.GetAllByTutorIdAsync(tutor.Id);
            if (!string.IsNullOrEmpty(timeId))
            {
                times = times.Where(t => t.Id == Convert.ToInt32(timeId));
            }

            if (!string.IsNullOrEmpty(subjectLevelId))
            {
                times = times.Where(t => t.Id == Convert.ToInt32(subjectLevelId));
            }

            if (from.HasValue)
            {
                times = times.Where(t => t.Date.Date >= from.Value);
            }

            if (to.HasValue)
            {
                times = times.Where(t => t.Date.Date <= to.Value);
            }

            times = times.OrderBy(t => t.Date.Date);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "des": times = times.OrderByDescending(x => x.Date.Date); break;
                }
            }

            var result = PaginatedList<Time>.Create(times, page, PAGE_SIZE);

            if (result.IsNullOrEmpty())
            {
                return new ResponseApiDTO<IEnumerable<TimeResponseDTO>>
                {
                    Success = true,
                    Message = "Hiện tại bạn chưa thêm lịch cho môn học này."
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

        public async Task<ResponseApiDTO> UpdateAsync(int timeId, TimeRequestDTO timeRequest)
        {
            var time = await _timeRepo.GetByIdAsync(timeId);

            if (time == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Không tồn tại lịch học với Id: {timeId}"
                };
            }

            var validTime = await CheckValidTime(timeRequest, time.SubjectLevel.TutorId);
            if(!validTime.Success)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = validTime.Message,
                };
            }

            var result = await _timeRepo.UpdateAsync(time);

            if(!result)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Hệ thống gặp lỗi khi cập nhật lịch học."
                };
            }

            var subjectLevel = await _subjectLevelRepo.GetByIdAsync(timeRequest.SubjectLevelId);

            if (subjectLevel == null)
            {
                return new ResponseApiDTO<TimeResponseDTO>
                {
                    Success = false,
                    Message = $"Không tồn tại khóa học với Id: {timeRequest.SubjectLevelId}."
                };
            }

            var studentJoins = subjectLevel.StudentJoins;
            foreach (var item in studentJoins)
            {
                var student = await _userRepo.GetByIdAsync(item.UserId);
                var title = $"Thư thông báo về khóa học {time.SubjectLevelId}!";
                var content = $@"
<p>- Giảng viên chủ nhiệm khóa học đã cập nhật lịch học của khóa.</p>
<p>- Sử dụng Id: <strong>{time.Id}</strong> vào mục Time để tra cứu thông tin chi tiết.</p>
<p>- Vui lòng thường xuyên kiểm tra Email bằng tài khoản này để cập nhật thông tin lớp học.</p>";
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
                Message = $"Cập nhật lịch học thành công."
            };
        }

        public async Task<ResponseApiDTO<IEnumerable<TimeResponseDTO>>> GetAllAsync(string? timeId, string? subjectLevelId, string? sortBy, DateTime? from, DateTime? to, int page = 1)
        {
            var times = await _timeRepo.GetAllAsync();

            if (!string.IsNullOrEmpty(timeId))
            {
                times = times.Where(t => t.Id == Convert.ToInt32(timeId));
            }

            if (!string.IsNullOrEmpty(subjectLevelId))
            {
                times = times.Where(t => t.Id == Convert.ToInt32(subjectLevelId));
            }

            if (from.HasValue)
            {
                times = times.Where(t => t.Date.Date >= from.Value);
            }

            if (to.HasValue)
            {
                times = times.Where(t => t.Date.Date <= to.Value);
            }

            times = times.OrderBy(t => t.Date.Date);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "des": times = times.OrderByDescending(x => x.Date.Date); break;
                }
            }

            var result = PaginatedList<Time>.Create(times, page, PAGE_SIZE);

            if (result.IsNullOrEmpty())
            {
                return new ResponseApiDTO<IEnumerable<TimeResponseDTO>>
                {
                    Success = true,
                    Message = "Hiện tại bạn chưa có lịch cho môn học này."
                };
            }

            return new ResponseApiDTO<IEnumerable<TimeResponseDTO>>
            {
                Success = true,
                Message = "Đây là danh sách các lịch học.",
                Data = result.Select(x => new TimeResponseDTO
                {
                    Id = x.Id,
                    SubjectLevelId = x.SubjectLevelId,
                    SlotName = x.SlotName,
                    StartSlot = x.StartSlot.TimeOfDay.ToString(@"hh\:mm\:ss"),
                    EndSlot = x.EndSlot.TimeOfDay.ToString(@"hh\:mm\:ss"),
                    Date = x.Date.Date.ToString("dd/MM/yyyy"),
                    IsLocked = x.SubjectLevel.Tutor.User.IsLocked,
                })
            };
        }

        public async Task<ResponseApiDTO> DeleteForTutorAsync(int timeId)
        {
            var time = await _timeRepo.GetByIdAsync(timeId);
            if(time == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Không tìm thấy lịch học trong hệ thống."
                };
            }
            var subjectLevel = await _subjectLevelRepo.GetByIdAsync(time.SubjectLevelId);
            var studentJoins = subjectLevel.StudentJoins;
            var delete = await _timeRepo.DeleteAsync(time);
            if(!delete)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Lỗi xảy ra khi xóa lịch học trong hệ thống."
                };
            }

            foreach (var studentJoin in studentJoins)
            {
                var student = await _userRepo.GetByIdAsync(studentJoin.UserId);
                var titleStudent = $"Thư thông báo xóa lịch học {time.SlotName} của lớp {subjectLevel.Id}!";
                var contentStudent = $@"
<p>- Hệ thống ghi nhận gia sư đã xóa lịch học <strong>{time.SlotName}</strong>.</p>
<p>- Mọi thông tin chi tiết vui lòng liên hệ với giảng viên của bạn.</p>
<p>- Email giảng viên: <a href='mailto:{studentJoin.SubjectLevel.Tutor.User.Email}'>{studentJoin.SubjectLevel.Tutor.User.Email}</a></p>
<p>- Vui lòng thường xuyên kiểm tra Email bằng tài khoản này để cập nhật thông tin lớp học.</p>";
                var messageStudent = new EmailDTO
                (
                    new string[] { student.Email! },
                        titleStudent,
                        contentStudent!
                );
                _emailService.SendEmail(messageStudent);
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = "Xóa lịch học thành công."
            };
        }

        public async Task<ResponseApiDTO> DeleteForStaffAsync(int timeId)
        {
            var time = await _timeRepo.GetByIdAsync(timeId);
            if (time == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Không tìm thấy lịch học trong hệ thống."
                };
            }
            var subjectLevel = await _subjectLevelRepo.GetByIdAsync(time.SubjectLevelId);
            var studentJoins = subjectLevel.StudentJoins;
            var delete = await _timeRepo.DeleteAsync(time);
            if (!delete)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Lỗi xảy ra khi xóa lịch học trong hệ thống."
                };
            }

            var studentMails = new List<string>();
            foreach (var studentJoin in studentJoins)
            {
                studentMails.Add(studentJoin.User.Email);
            }

            var titleStudent = $"Thư thông báo xóa lịch học {time.SlotName} của lớp {subjectLevel.Id}!";
            var contentStudent = $@"
<p>- Hệ thống ghi nhận gia sư đã xóa lịch học <strong>{time.SlotName}</strong>.</p>
<p>- Mọi thông tin chi tiết vui lòng liên hệ với giảng viên của bạn hoặc phản hồi lại mail này để được giải đáp.</p>
<p>- Email giảng viên: <a href='mailto:{subjectLevel.Tutor.User.Email}'>{subjectLevel.Tutor.User.Email}</a></p>
<p>- Vui lòng thường xuyên kiểm tra Email bằng tài khoản này để cập nhật thông tin lớp học.</p>";
            var messageStudent = new EmailDTO
            (
                studentMails,
                    titleStudent,
                    contentStudent!
            );
            _emailService.SendEmail(messageStudent);

            var tutorEmail = subjectLevel.Tutor.User.Email;
            var titleTutor = $"Thư thông báo xóa lịch học {time.SlotName} của lớp {subjectLevel.Id}!";
            var contentTutor = $@"
<p>- Hệ thống lịch học <strong>{time.SlotName}</strong>.</p>
<p>- Mọi thông tin chi tiết vui lòng phản hồi lại mail này để được giải đáp.</p>
<p>- Vui lòng thường xuyên kiểm tra Email bằng tài khoản này để cập nhật thông tin lớp học.</p>";
            var messageTutor = new EmailDTO
            (
                new string[] { tutorEmail! },
                    titleTutor,
                    contentTutor!
            );
            _emailService.SendEmail(messageTutor);

            return new ResponseApiDTO
            {
                Success = true,
                Message = "Xóa lịch học thành công."
            };
        }
    }
}
