﻿using AutoMapper;
using Mailjet.Client.Resources;
using Microsoft.IdentityModel.Tokens;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Helper;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.DataAccessLayer.DAO;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;
using OnDemandTutorApi.DataAccessLayer.Repositories.RepoImpl;
using Org.BouncyCastle.Utilities;

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

        public static int PAGE_SIZE { get; set; } = 5;

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
        public async Task<ResponseApiDTO<StudentJoinResponseDTO>> CreateAsync(StudentJoinRequestDTO studentJoinDTO)
        {
            var existSubjectLevel = await _subjectLevelRepo.GetByIdAsync(studentJoinDTO.SubjectLevelId);

            if(existSubjectLevel == null)
            {
                return new ResponseApiDTO<StudentJoinResponseDTO>
                {
                    Success = false,
                    Message = $"Không tồn tại khóa học với Id: {studentJoinDTO.SubjectLevelId} trong hệ thống."
                };
            }

            var existStudentJoins = await _studentJoinRepo.GetAllBySubjectLevelIdAsync(studentJoinDTO.SubjectLevelId);
            if(existStudentJoins.Count() >= existSubjectLevel.LimitMember)
            {
                return new ResponseApiDTO<StudentJoinResponseDTO>
                {
                    Success = false,
                    Message = "Khóa đã đủ học sinh tham gia."
                };
            }
            foreach (var join in existStudentJoins)
            {
                if (join.UserId == studentJoinDTO.UserId)
                {
                    return new ResponseApiDTO<StudentJoinResponseDTO>
                    {
                        Success = false,
                        Message = "Bạn đã đăng kí khóa học này rồi."
                    };
                }
            }

            var transactionStudent = await _coinManagementService.DepositAsync(new CoinDTO
            {
                UserId = studentJoinDTO.UserId,
                Coin = -existSubjectLevel.Coin,
            });

            if (!transactionStudent.Success)
            {
                return new ResponseApiDTO<StudentJoinResponseDTO>
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
                return new ResponseApiDTO<StudentJoinResponseDTO>
                {
                    Success = false,
                    Message = "Giao dịch không thành công. Vui lòng thử lại lần nữa."
                };
            }

            var studentJoin = _mapper.Map<StudentJoin>(studentJoinDTO);
            var result = await _studentJoinRepo.CreateAsync(studentJoin);

            if(!result)
            {
                return new ResponseApiDTO<StudentJoinResponseDTO>
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

            return new ResponseApiDTO<StudentJoinResponseDTO>
            {
                Success = true,
                Message = "Đăng kí mua thành công. Đây là thông tin đăng kí của bạn.",
                Data = new StudentJoinResponseDTO
                {
                    Id = studentJoin.Id,
                    UserId = studentJoin.UserId,
                    SubjectLevelId = studentJoin.SubjectLevelId,
                    FullName = student.FullName,
                    Email = student.Email,
                }
            };
        }

        public async Task<ResponseApiDTO<IEnumerable<StudentJoinResponseDTO>>> GetAllBySubjectLevelIdAsync(string subjectLevelId, string? userId, int page = 1)
        {
            var studentJoins = await _studentJoinRepo.GetAllBySubjectLevelIdAsync(Convert.ToInt32(subjectLevelId));

            if(!string.IsNullOrEmpty(userId))
            {
                studentJoins = studentJoins.Where(x => x.UserId == userId);
            }

            var result = PaginatedList<StudentJoin>.Create(studentJoins, page, PAGE_SIZE);
            if (result.IsNullOrEmpty())
            {
                return new ResponseApiDTO<IEnumerable<StudentJoinResponseDTO>>
                {
                    Success = true,
                    Message = "Hiện tại chưa có học sinh nào đăng kí khóa học."
                };
            }

            return new ResponseApiDTO<IEnumerable<StudentJoinResponseDTO>>
            {
                Success = true,
                Message = "Đây là danh sách các học sinh đã đăng kí khóa học",
                Data = result.Select(x => new StudentJoinResponseDTO
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    SubjectLevelId = x.SubjectLevelId,
                    FullName = x.User.FullName,
                    Email = x.User.Email,
                })
            };
        }

        public async Task<ResponseApiDTO<IEnumerable<StudentJoinResponseDTO>>> GetAllAsync(string? subjectLevelId, string? userId, int page = 1)
        {
            var studentJoins = await _studentJoinRepo.GetAllAsync();

            if (!string.IsNullOrEmpty(subjectLevelId))
            {
                studentJoins = studentJoins.Where(x => x.SubjectLevelId == Convert.ToInt32(subjectLevelId));
            }

            if (!string.IsNullOrEmpty(userId))
            {
                studentJoins = studentJoins.Where(x => x.UserId == userId);
            }

            var result = PaginatedList<StudentJoin>.Create(studentJoins, page, PAGE_SIZE);
            if (result.IsNullOrEmpty())
            {
                return new ResponseApiDTO<IEnumerable<StudentJoinResponseDTO>>
                {
                    Success = true,
                    Message = "Hiện tại chưa có học sinh nào đăng kí các khóa học."
                };
            }

            return new ResponseApiDTO<IEnumerable<StudentJoinResponseDTO>>
            {
                Success = true,
                Message = "Đây là danh sách các học sinh đã đăng kí các khóa học",
                Data = result.Select(x => new StudentJoinResponseDTO
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    SubjectLevelId = x.SubjectLevelId,
                    FullName = x.User.FullName,
                    Email = x.User.Email,
                })
            };
        }

        public async Task<ResponseApiDTO> DeleteForTutorAsync(int studentJoinId)
        {
            var studentJoin = await _studentJoinRepo.GetByIdAsync(studentJoinId);

            if(studentJoin == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Không tồn tại học sinh này trong lớp."
                };
            }

            var result = await _studentJoinRepo.DeleteAsync(studentJoin);

            if(!result)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Hệ thống gặp lỗi khi xóa học sinh này khỏi lớp."
                };
            }

            var student = await _userRepo.GetByIdAsync(studentJoin.UserId);
            var titleStudent = $"Thư thông báo xóa học sinh khỏi lớp học {studentJoin.SubjectLevelId}!";
            var contentStudent = @$"- Hệ thống ghi nhận bạn đã bị giảng viên xóa khỏi lớp.
                             - Mọi thông tin chi tiết vui lòng liên hệ với giảng viên của bạn.
                             - Email giảng viên: {studentJoin.SubjectLevel.Tutor.User.Email}
                             - Vui lòng thường xuyên kiểm tra Email bằng tài khoản này để cập nhật thông tin lớp học.";
            var messageStudent = new EmailDTO
            (
                new string[] { student.Email! },
                    titleStudent,
                    contentStudent!
            );
            _emailService.SendEmail(messageStudent);

            return new ResponseApiDTO
            {
                Success = true,
                Message = "Xóa học sinh khỏi lớp thành công."
            };
        }

        public async Task<ResponseApiDTO> DeleteForStudentAsync(int studentJoinId)
        {
            var studentJoin = await _studentJoinRepo.GetByIdAsync(studentJoinId);

            if (studentJoin == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Bạn chưa đăng kí khóa học này."
                };
            }

            var result = await _studentJoinRepo.DeleteAsync(studentJoin);

            if (!result)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Hệ thống gặp lỗi khi xóa bạn khỏi lớp."
                };
            }

            var student = await _userRepo.GetByIdAsync(studentJoin.UserId);
            var titleStudent = $"Thư thông báo học sinh đã rời khỏi lớp học {studentJoin.SubjectLevelId}!";
            var contentStudent = @$"- Hệ thống ghi nhận bạn đã rời khỏi lớp.
                             - Mọi thông tin chi tiết vui lòng liên hệ với giảng viên của bạn.
                             - Email giảng viên: {studentJoin.SubjectLevel.Tutor.User.Email}
                             - Vui lòng thường xuyên kiểm tra Email bằng tài khoản này để cập nhật thông tin lớp học.";
            var messageStudent = new EmailDTO
            (
                new string[] { student.Email! },
                    titleStudent,
                    contentStudent!
            );
            _emailService.SendEmail(messageStudent);

            var tutor = studentJoin.SubjectLevel.Tutor;
            var titleTutor = $"Thư thông báo học sinh đã rời khỏi lớp học {studentJoin.SubjectLevelId}!";
            var contentTutor = @$"- Hệ thống ghi nhận học sinh với Id: {studentJoin.UserId} đã rời khỏi lớp.
                             - Mọi thông tin chi tiết vui lòng liên hệ với học sinh của bạn.
                             - Email học sinh: {studentJoin.User.Email}
                             - Vui lòng thường xuyên kiểm tra Email bằng tài khoản này để cập nhật thông tin lớp học.";
            var messageTutor = new EmailDTO
            (
                new string[] { tutor.User.Email! },
                    titleTutor,
                    contentTutor!
            );
            _emailService.SendEmail(messageTutor);

            return new ResponseApiDTO
            {
                Success = true,
                Message = "Xóa học sinh khỏi lớp thành công."
            };
        }
    }
}
