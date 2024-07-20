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
        private readonly ISubjectLevelRepo _subjectLevelRepo;
        private readonly IStudentJoinRepo _studentJoinRepo;
        private readonly ITimeRepo _timeRepo;
        private readonly IUserRepo _userRepo;
        private readonly IEmailService _emailService;

        public static int PAGE_SIZE { get; set; } = 10;

        public LevelService(ILevelRepo levelRepo, IMapper mapper, ISubjectLevelRepo subjectLevelRepo
                            ,IStudentJoinRepo studentJoinRepo, ITimeRepo timeRepo, IUserRepo userRepo,
                            IEmailService emailService)
        {
            _levelRepo = levelRepo;
            _mapper = mapper;
            _subjectLevelRepo = subjectLevelRepo;
            _studentJoinRepo = studentJoinRepo;
            _timeRepo = timeRepo;
            _userRepo = userRepo;
            _emailService = emailService;
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
                int id;
                if(int.TryParse(search, out id))
                {
                    levels = levels.Where(x => x.Id == id);
                }
                else
                {
                    levels = levels.Where(x => x.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                
            }

            if (!string.IsNullOrEmpty(levelId))
            {
                int id;
                if (int.TryParse(levelId, out id))
                {
                    levels = levels.Where(x => x.Id == id);
                }
                
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
                }),
                Total = levels.Count()
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

        public async Task<ResponseApiDTO> DeleteAsync(int id)
        {
            var level = await _levelRepo.GetByIdAsync(id);
            if(level == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Không tồn tại cấp bậc trong hệ thống."
                };
            }


            var subjectLevelsToDelete = level.SubjectLevels.ToList();
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

            // Tải lại level để kiểm tra lại các danh sách sau khi xóa
            level = await _levelRepo.GetByIdAsync(id);
            var remainingTimes = level.SubjectLevels.SelectMany(sl => sl.Times).ToList();
            var remainingStudentJoins = level.SubjectLevels.SelectMany(sl => sl.StudentJoins).ToList();

            if (remainingTimes.Any() || remainingStudentJoins.Any())
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Lỗi xảy ra khi cố xóa các lịch học và học sinh liên quan đến khóa học ở cấp bậc này."
                };
            }

            foreach (var subjectLevel in subjectLevelsToDelete)
            {
                await _subjectLevelRepo.DeleteAsync(subjectLevel);
            }

            // Tải lại level để kiểm tra lại các danh sách subjectLevels sau khi xóa
            level = await _levelRepo.GetByIdAsync(id);
            var remainingSubjectLevels = level.SubjectLevels.ToList();


            if (remainingSubjectLevels.Any())
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Lỗi xảy ra khi cố xóa các khóa học ở cấp bậc này."
                };
            }

            var result = await _levelRepo.DeleteAsync(level);
            if(!result)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Lỗi xảy ra khi cố xóa cấp bậc này."
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
<p>- Do cấp bậc <strong>{level.Name}</strong> đã bị xóa.</p>
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
<p>- Do cấp bậc <strong>{level.Name}</strong> đã bị xóa.</p>
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
