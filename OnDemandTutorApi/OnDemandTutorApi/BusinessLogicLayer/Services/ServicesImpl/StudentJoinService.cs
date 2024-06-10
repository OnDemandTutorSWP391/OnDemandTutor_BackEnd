using AutoMapper;
using Mailjet.Client.Resources;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;
using OnDemandTutorApi.DataAccessLayer.Repositories.RepoImpl;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl
{
    public class StudentJoinService : IStudentJoinService
    {
        private readonly IStudentJoinRepo _studentJoinRepo;
        private readonly IMapper _mapper;
        private readonly ISubjectLevelRepo _subjectLevelRepo;
        private readonly ICoinManagementService _coinManagementService;
        private readonly IEmailService _emailService;
        private readonly IUserRepo _userRepo;
        private readonly ITutorRepo _tutorRepo;

        public StudentJoinService(IStudentJoinRepo studentJoinRepo, IMapper mapper, ISubjectLevelRepo subjectLevelRepo,
            ICoinManagementService coinManagementService, IEmailService emailService, IUserRepo userRepo, ITutorRepo tutorRepo)
        {
            _studentJoinRepo = studentJoinRepo;
            _mapper = mapper;
            _subjectLevelRepo = subjectLevelRepo;
            _coinManagementService = coinManagementService;
            _emailService = emailService;
            _userRepo = userRepo;
            _tutorRepo = tutorRepo;
        }
        public async Task<ResponseApiDTO<StudentJoinDTOWithId>> CreateAsync(StudentJoinDTO studentJoinDTO)
        {
            var existSubjectLevel = await _subjectLevelRepo.GetByIdAsync(studentJoinDTO.SubjectLevelId);

            if(existSubjectLevel == null)
            {
                return new ResponseApiDTO<StudentJoinDTOWithId>
                {
                    Success = false,
                    Message = $"Không tồn tại khóa học với Id: {studentJoinDTO.SubjectLevelId} trong hệ thống."
                };
            }

            var transactionStudent = await _coinManagementService.DepositAsync(new CoinDTO
            {
                UserId = studentJoinDTO.UserId,
                Coin = -existSubjectLevel.Coin,
            });

            if (!transactionStudent.Success)
            {
                return new ResponseApiDTO<StudentJoinDTOWithId>
                {
                    Success = false,
                    Message = "Giao dịch không thành công. Vui lòng thử lại lần nữa."
                };
            }

            var transactionTutor = await _coinManagementService.DepositAsync(new CoinDTO
            {
                UserId = existSubjectLevel.Tutor.User.Id,
                Coin = existSubjectLevel.Coin,
            });

            if (!transactionTutor.Success)
            {
                return new ResponseApiDTO<StudentJoinDTOWithId>
                {
                    Success = false,
                    Message = "Giao dịch không thành công. Vui lòng thử lại lần nữa."
                };
            }


            var studentJoin = _mapper.Map<StudentJoin>(studentJoinDTO);
            var result = await _studentJoinRepo.CreateAsync(studentJoin);

            if(!result)
            {
                return new ResponseApiDTO<StudentJoinDTOWithId>
                {
                    Success = false,
                    Message = $"Hệ thống gặp lỗi trong quá trình mua khóa học."
                };
            }

            var student = await _userRepo.GetByIdAsync(studentJoinDTO.UserId);
            var titleStudent = $"Thư xác nhận đăng kí khóa học {studentJoinDTO.SubjectLevelId} thành công!";
            var contentStudent = @$"- Hệ thống đã xác nhận đăng kí mua khóa học thành công.
                             - Url tham gia lớp học của bạn {existSubjectLevel.Url}.
                             - Vui lòng thường xuyên kiểm tra Email bằng tài khoản này để cập nhật thông tin lớp học.";
            var messageStudent = new EmailDTO
            (
                new string[] { student.Email! },
                    titleStudent,
                    contentStudent!
            );
            _emailService.SendEmail(messageStudent);

            var tutor = await _tutorRepo.GetByIdAsync(existSubjectLevel.TutorId);
            var titleTutor = $"Thư thông báo học sinh đăng kí khóa học {studentJoinDTO.SubjectLevelId} thành công!";
            var contentTutor = @$"- Hệ thống đã xác nhận học sinh đăng kí mua khóa học thành công.
                             - Id của học sinh: {student.Id}.
                             - Email của học sinh: {student.Email}
                             - Vui lòng trao đổi và gửi thông tin cũng như lịch dạy qua Email của học sinh.";
            var messageTutor = new EmailDTO
            (
                new string[] { tutor.User.Email! },
                    titleTutor,
                    contentTutor!
            );
            _emailService.SendEmail(messageTutor);

            return new ResponseApiDTO<StudentJoinDTOWithId>
            {
                Success = true,
                Message = "Đăng kí mua thành công. Đây là thông tin đăng kí của bạn.",
                Data = new StudentJoinDTOWithId
                {
                    Id = studentJoin.Id,
                    UserId = studentJoin.UserId,
                    SubjectLevelId = studentJoin.SubjectLevelId,
                }
            };
        }
    }
}
