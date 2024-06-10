using AutoMapper;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
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
    }
}
