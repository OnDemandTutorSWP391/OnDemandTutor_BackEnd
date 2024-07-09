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
        private readonly ISubjectLevelRepo _subjectLevelRepo;
        private readonly IStudentJoinRepo _studentJoinRepo;
        private readonly ITimeRepo _timeRepo;
        private readonly IUserRepo _userRepo;
        private readonly IEmailService _emailService;

        public static int PAGE_SIZE { get; set; } = 5;

        public SubjectService(ISubjectRepo subjectRepo, IMapper mapper, ISubjectLevelRepo subjectLevelRepo
                            , IStudentJoinRepo studentJoinRepo, ITimeRepo timeRepo, IUserRepo userRepo,
                            IEmailService emailService)
        {
            _subjectRepo = subjectRepo;
            _mapper = mapper;
            _subjectLevelRepo = subjectLevelRepo;
            _studentJoinRepo = studentJoinRepo;
            _timeRepo = timeRepo;
            _userRepo = userRepo;
            _emailService = emailService;
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
                subjects = subjects.Where(x => x.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (!string.IsNullOrEmpty(subjectId))
            {
                int id;
                if(int.TryParse(subjectId, out id))
                {
                    subjects = subjects.Where(x => x.Id == id);
                }
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

        public async Task<ResponseApiDTO> DeleteAsync(int id)
        {
            var subject = await _subjectRepo.GetByIdAsync(id);
            if (subject == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Không tồn tại môn học trong hệ thống."
                };
            }

            var subjectLevelsToDelete = subject.SubjectLevels.ToList();
            var studentJoinsToDelete = subjectLevelsToDelete.SelectMany(sl => sl.StudentJoins).ToList();
            var timesToDelete = subjectLevelsToDelete.SelectMany(sl => sl.Times).ToList();
            

            foreach (var time in timesToDelete)
            {
                await _timeRepo.DeleteAsync(time);
            }

            foreach (var studentJoin in studentJoinsToDelete)
            {
                await _studentJoinRepo.DeleteAsync(studentJoin);
            }

            subject = await _subjectRepo.GetByIdAsync(id);
            var remainingTimes = subject.SubjectLevels.SelectMany(sl => sl.Times).ToList();
            var remainingStudentJoins = subject.SubjectLevels.SelectMany(sl => sl.StudentJoins).ToList();

            if (remainingTimes.Any() || remainingStudentJoins.Any())
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Lỗi xảy ra khi cố xóa các lịch học và học sinh liên quan đến khóa học ở môn học này."
                };
            }

            foreach (var subjectLevel in subjectLevelsToDelete)
            {
                await _subjectLevelRepo.DeleteAsync(subjectLevel);
            }

            subject = await _subjectRepo.GetByIdAsync(id);
            var remainingSubjectLevels = subject.SubjectLevels.ToList();

            if (remainingSubjectLevels.Any())
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Lỗi xảy ra khi cố xóa các khóa học ở môn học này."
                };
            }

            var result = await _subjectRepo.DeleteAsync(subject);
            if (!result)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Lỗi xảy ra khi cố xóa môn học này."
                };
            }

            foreach (var subjectLevel in subjectLevelsToDelete)
            {
                foreach (var studentJoin in studentJoinsToDelete)
                {
                    var student = await _userRepo.GetByIdAsync(studentJoin.UserId);
                    var titleStudent = $"Thư thông báo xóa học sinh khỏi lớp học {studentJoin.SubjectLevelId}!";
                    var contentStudent = $@"
<p>- Hệ thống ghi nhận bạn đã bị xóa khỏi lớp.</p>
<p>- Do môn học <strong>{subject.Name}</strong> đã bị xóa.</p>
<p>- Vì vậy lớp học <strong>{id}</strong> của gia sư <strong>{subjectLevel.Tutor.User.FullName}</strong> đã bị xóa.</p>
<p>- Mọi thông tin chi tiết vui lòng liên hệ với giảng viên của bạn hoặc phản hồi lại mail này để được giải đáp.</p>
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

                var tutor = subjectLevel.Tutor;
                var titleTutor = $"Thư thông báo xóa lớp học {subjectLevel.Id}!";
                var contentTutor = $@"
<p>- Hệ thống ghi nhận bạn đã xóa lớp học này của bạn.</p>
<p>- Do cấp bậc <strong>{subject.Name}</strong> đã bị xóa.</p>
<p>- Vì vậy lớp học <strong>{id}</strong> của gia sư <strong>{subjectLevel.Tutor.User.FullName}</strong> đã bị xóa.</p>
<p>- Mọi thông tin chi tiết vui lòng phản hồi lại mail này để được giải đáp.</p>
<p>- Vui lòng thường xuyên kiểm tra Email bằng tài khoản này để cập nhật thông tin về website.</p>";
                var messageTutor = new EmailDTO
                (
                    new string[] { tutor.User.Email! },
                        titleTutor,
                        contentTutor!
                );
                _emailService.SendEmail(messageTutor);
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = "Xóa cấp bậc này thành công."
            };
        }
    }
}

