using AutoMapper;
using Mailjet.Client.Resources;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Helper;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;
using OnDemandTutorApi.DataAccessLayer.Repositories.RepoImpl;


namespace OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl
{
    public class SubjectLevelService : ISubjectLevelService
    {
        private readonly ISubjectLevelRepo _subjectLevelRepo;
        private readonly ITutorRepo _tutorRepo;
        private readonly ILevelRepo _levelRepo;
        private readonly ISubjectRepo _subjectRepo;
        private readonly IMapper _mapper;
        private readonly IStudentJoinRepo _studentJoinRepo;
        private readonly ITimeRepo _timeRepo;
        private readonly IUserRepo _userRepo;
        private readonly IEmailService _emailService;

        public static int PAGE_SIZE { get; set; } = 5;

        public SubjectLevelService(ISubjectLevelRepo subjectLevelRepo, ITutorRepo tutorRepo, ILevelRepo levelRepo, 
            ISubjectRepo subjectRepo, IMapper mapper, IStudentJoinRepo studentJoinRepo, ITimeRepo timeRepo,
            IUserRepo userRepo, IEmailService emailService)
        {
            _subjectLevelRepo = subjectLevelRepo;
            _tutorRepo = tutorRepo;
            _levelRepo = levelRepo;
            _subjectRepo = subjectRepo;
            _mapper = mapper;
            _studentJoinRepo = studentJoinRepo;
            _timeRepo = timeRepo;
            _userRepo = userRepo;
            _emailService = emailService;
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
                    ServiceName = subjectLevel.Name,
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
            subjectLevels = subjectLevels.Where(x => x.Tutor.User.IsLocked == false);

            if (!string.IsNullOrEmpty(level))
            {
                subjectLevels = subjectLevels.Where(x => x.Level.Name.IndexOf(level, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (!string.IsNullOrEmpty(subject))
            {
                subjectLevels = subjectLevels.Where(x => x.Subject.Name.IndexOf(subject, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (!string.IsNullOrEmpty(tutor))
            {
                subjectLevels = subjectLevels.Where(x => x.Tutor.User.FullName.IndexOf(tutor, StringComparison.OrdinalIgnoreCase) >= 0);
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
                subjectLevelResponse.ServiceName = subjectLevel.Name;
                subjectLevelResponse.LimitMember = $"{count}/{subjectLevel.LimitMember}";
                subjectLevelResponse.IsLocked = subjectLevel.Tutor.User.IsLocked;
                subjectLevelResponses.Add(subjectLevelResponse);
            }

            return new ResponseApiDTO<IEnumerable<SubjectLevelResponseDTO>>
            {
                Success = true,
                Message = "Đây là danh sách các môn học của hệ thống",
                Data = subjectLevelResponses
            };
        }

        public async Task<ResponseApiDTO<IEnumerable<SubjectLevelResponseDTO>>> GetAllForStaffAsync(string? level, string? subject, string? tutor, int page = 1)
        {
            var subjectLevels = await _subjectLevelRepo.GetAllAsync();

            if (!string.IsNullOrEmpty(level))
            {
                subjectLevels = subjectLevels.Where(x => x.Level.Name.IndexOf(level, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (!string.IsNullOrEmpty(subject))
            {
                subjectLevels = subjectLevels.Where(x => x.Subject.Name.IndexOf(subject, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (!string.IsNullOrEmpty(tutor))
            {
                subjectLevels = subjectLevels.Where(x => x.Tutor.User.FullName.IndexOf(tutor, StringComparison.OrdinalIgnoreCase) >= 0);
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
                subjectLevelResponse.ServiceName = subjectLevel.Name;
                subjectLevelResponse.LimitMember = $"{count}/{subjectLevel.LimitMember}";
                subjectLevelResponse.IsLocked = subjectLevel.Tutor.User.IsLocked;
                subjectLevelResponses.Add(subjectLevelResponse);
            }

            return new ResponseApiDTO<IEnumerable<SubjectLevelResponseDTO>>
            {
                Success = true,
                Message = "Đây là danh sách các môn học của hệ thống",
                Data = subjectLevelResponses
            };
        }

        public async Task<ResponseApiDTO<SubjectLevelResponseDTO>> GetByIdAsync(int subjectLevelId)
        {
            var subjectLevel = await _subjectLevelRepo.GetByIdAsync(subjectLevelId);

            if (subjectLevel == null)
            {
                return new ResponseApiDTO<SubjectLevelResponseDTO>
                {
                    Success = false,
                    Message = $"Không tồn tại khóa học với Id: {subjectLevelId}"
                };
            }

            return new ResponseApiDTO<SubjectLevelResponseDTO>
            {
                Success = true,
                Message = $"Đây là khóa học khớp với Id: {subjectLevelId}",
                Data = new SubjectLevelResponseDTO
                {
                    Id = subjectLevelId,
                    LevelName = subjectLevel.Level.Name,
                    SubjectName = subjectLevel.Level.Name,
                    TutorName = subjectLevel.Tutor.User.FullName,
                    ServiceName = subjectLevel.Name,
                    Description = subjectLevel.Description,
                    Url = subjectLevel.Url,
                    Coin = subjectLevel.Coin,
                    LimitMember = $"{subjectLevel.StudentJoins.Count}/{subjectLevel.LimitMember}",
                }
            };
        }

        public async Task<ResponseApiDTO> UpdateForTutorAsync(int id, string userId, SubjectLevelRequestDTO subjectLevelDTO)
        {
            var subjectLevel = await _subjectLevelRepo.GetByIdAsync(id);

            if(subjectLevel == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Không tồn tại lớp học của bạn với mã {id}."
                };
            }

            if(subjectLevel.Tutor.User.Id != userId)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Bạn không thể cập nhật thông tin lớp học của giảng viên khác."
                };
            }

            var update = _mapper.Map(subjectLevelDTO, subjectLevel);

            var result = await _subjectLevelRepo.UpdateAsync(update);

            if(!result)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Hệ thống gặp lỗi khi cập nhật lớp học của bạn."
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = "Cập nhật thông tin lớp học của bạn thành công."
            };
        }

        public async Task<ResponseApiDTO> DeleteForTutorAsync(int id)
        {
            var subjectLevel = await _subjectLevelRepo.GetByIdAsync(id);

            if (subjectLevel == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Khóa học không tồn tại trong hệ thống."
                };
            }

            var studentJoinsPreDelete = subjectLevel.StudentJoins.ToList();
            var studentJoinsToDelete = subjectLevel.StudentJoins.ToList();
            var timesToDelete = subjectLevel.Times.ToList();

            foreach (var time in timesToDelete)
            {
                await _timeRepo.DeleteAsync(time);
            }

            foreach (var studentJoin in studentJoinsToDelete)
            {
                await _studentJoinRepo.DeleteAsync(studentJoin);
            }

            // Tải lại subjectLevel để kiểm tra lại các danh sách
            subjectLevel = await _subjectLevelRepo.GetByIdAsync(id);

            // Kiểm tra lại các danh sách sau khi đã xóa
            var remainingTimes = subjectLevel.Times;
            var remainingStudentJoins = subjectLevel.StudentJoins;

            if (remainingStudentJoins.Any() || remainingTimes.Any())
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Lỗi xảy ra khi xóa các lịch học và học sinh liên quan đến khóa học."
                };
            }

            var result = await _subjectLevelRepo.DeleteAsync(subjectLevel);

            if (!result)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Lỗi xảy ra khi xóa khóa học."
                };
            }

            foreach (var studentJoin in studentJoinsPreDelete)
            {
                var student = await _userRepo.GetByIdAsync(studentJoin.UserId);
                var titleStudent = $"Thư thông báo xóa học sinh khỏi lớp học {studentJoin.SubjectLevelId}!";
                var contentStudent = $@"
<p>- Hệ thống ghi nhận bạn đã bị xóa khỏi lớp.</p>
<p>- Do lớp học <strong>{id}</strong> của gia sư <strong>{subjectLevel.Tutor.User.FullName}</strong> đã bị xóa.</p>
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
                Message = "Xóa khóa học thành công."
            };
        }

        public async Task<ResponseApiDTO> DeleteForStaffAsync(int id)
        {
            var subjectLevel = await _subjectLevelRepo.GetByIdAsync(id);

            if (subjectLevel == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Khóa học không tồn tại trong hệ thống."
                };
            }

            var studentJoinsPreDelete = subjectLevel.StudentJoins;
            var studentJoinsToDelete = subjectLevel.StudentJoins;
            var timesToDelete = subjectLevel.Times;

            foreach (var time in timesToDelete)
            {
                await _timeRepo.DeleteAsync(time);
            }

            foreach (var studentJoin in studentJoinsToDelete)
            {
                await _studentJoinRepo.DeleteAsync(studentJoin);
            }

            if (studentJoinsToDelete.Any() || timesToDelete.Any())
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Lỗi xảy ra khi xóa các lịch học và học sinh liên quan đến khóa học."
                };
            }

            var result = await _subjectLevelRepo.DeleteAsync(subjectLevel);

            if (!result)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Lỗi xảy ra khi xóa khóa học."
                };
            }

            var studentEmails = new List<string>();
            foreach (var studentJoin in studentJoinsPreDelete)
            {
                studentEmails.Add(studentJoin.User.Email);
            }

            var titleStudent = $"Thư thông báo xóa học sinh khỏi lớp học {subjectLevel.Id}!";
            var contentStudent = $@"
<p>- Hệ thống ghi nhận bạn đã bị xóa khỏi lớp.</p>
<p>- Do lớp học <strong>{id}</strong> của gia sư <strong>{subjectLevel.Tutor.User.FullName}</strong> đã bị xóa.</p>
<p>- Mọi thông tin chi tiết vui lòng liên hệ với giảng viên của bạn hoặc phản hồi mail này để được giải đáp.</p>
<p>- Email giảng viên: <a href='mailto:{subjectLevel.Tutor.User.Email}'>{subjectLevel.Tutor.User.Email}</a></p>
<p>- Vui lòng thường xuyên kiểm tra Email bằng tài khoản này để cập nhật thông tin lớp học.</p>";
            var messageStudent = new EmailDTO
            (
                studentEmails,
                    titleStudent,
                    contentStudent!
            );
            _emailService.SendEmail(messageStudent);
            //
            var tutorEmail = subjectLevel.Tutor.User.Email;
            var titleTutor = $"Thư thông báo xóa lớp học {subjectLevel.Id}!";
            var contentTutor = $@"
<p>- Hệ thống ghi nhận lớp học đã bị xóa.</p>
<p>- Mọi thông tin chi tiết vui lòng phản hồi mail này để được giải đáp.</p>
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
                Message = "Xóa khóa học thành công."
            };
        }
    }
}
