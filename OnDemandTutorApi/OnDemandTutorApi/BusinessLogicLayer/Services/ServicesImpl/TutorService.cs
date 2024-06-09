using AutoMapper;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;
using OnDemandTutorApi.DataAccessLayer.Repositories.RepoImpl;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl
{
    public class TutorService : ITutorService
    {
        private readonly ITutorRepo _tutorRepo;
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;
        private readonly IRequestCategoryRepo _categoryRepo;
        private readonly IRequestRepo _requestRepo;

        public TutorService(ITutorRepo tutorRepo, IMapper mapper, IUserRepo userRepo, IRequestCategoryRepo categoryRepo, IRequestRepo requestRepo)
        {
            _tutorRepo = tutorRepo;
            _mapper = mapper;
            _userRepo = userRepo;
            _categoryRepo = categoryRepo;
            _requestRepo = requestRepo;
        }
        public async Task<int> AddTutorAsync(TutorDTO tutorDTO)
        {
            var tutor = _mapper.Map<Tutor>(tutorDTO);

            // Kiểm tra xem user đã là tutor hay chưa thông qua UserId
            var existingTutor = await GetTutorByUserIdAsync(tutor.UserId);

            if (existingTutor == null)
            {
                // Người dùng chưa là tutor, thêm tutor mới
                await _tutorRepo.AddTutorAsync(tutor);
                return tutor.TutorId;
            }
            else
            {
                // Người dùng đã là tutor, không thêm tutor mới
                return existingTutor.TutorId; // Trả về ID của tutor hiện tại
            }
        }

        public async Task<Tutor> GetTutorByIdAsync(int id)
        {
            var tutor = await _tutorRepo.GetByIdAsync(id);
            if (tutor == null)
            {
                return null;
            }

            return tutor;
        }


        public async Task DeleteTutorAsync(int id)
        {
            var tutor = await GetTutorByIdAsync(id);

            if (tutor != null)
            {
                await _tutorRepo.DeleteTutorAsync(tutor);
            }

        }

        public async Task<Tutor> GetTutorByUserIdAsync(string userId)
        {
            var tutor = await _tutorRepo.GetTutorByUserIdAsync(userId);
            if(tutor == null)
            {
                return null ;
            }

            return tutor;
        }

        public async Task<ResponseApiDTO> UpdateProfileAsync(string userId, ProfileRequestDTO profileTutor)
        {
            var tutor = await _tutorRepo.GetTutorByUserIdAsync(userId);

            if(tutor == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Hệ thống không tìm thấy bạn với vai trò giảng viên"
                };
            }

            var tutorRequest = _mapper.Map(profileTutor, tutor);
            tutorRequest.Status = "Chờ phê duyệt";
            var updateProfile = await _tutorRepo.UpdateTutorAsync(tutorRequest);
            if(!updateProfile)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Hệ thống gặp lỗi khi cập nhật thông tin của bạn."
                };
            }

            var requestDTO = new RequestDTO
            {
                Description = $"Yêu cầu phê duyệt dịch vụ giảng viên với TutorId: {tutor.TutorId}."
            };

            var category = await _categoryRepo.GetByNameAsync("Rental Service Support");
            var request = _mapper.Map<Request>(requestDTO);
            request.UserId = userId;
            request.RequestCategoryId = category.Id;

            var result = await _requestRepo.CreateAsync(request);

            if (!result)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Lỗi hệ thống khi gửi yêu cầu."
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = $"Bạn đã gửi yêu cầu đăng kí giảng viên thành công."
            };
        }

        public async Task<ResponseApiDTO<ProfileRequestDTO>> GetProfileByIdAsync(int id)
        {
            var tutor = await _tutorRepo.GetByIdAsync(id);

            if(tutor == null)
            {
                return new ResponseApiDTO<ProfileRequestDTO>
                {
                    Success = false,
                    Message = $"Không tìm thấy tutor nào với Id: {id}."
                };
            }

            var profile = _mapper.Map<ProfileRequestDTO>(tutor);

            return new ResponseApiDTO<ProfileRequestDTO>
            {
                Success = true,
                Message = $"Tutor {id} có profile như sau.",
                Data = profile
            };
        }

        public async Task<ResponseApiDTO<ProfileResponseDTO>> GetProfileAsync(string userId)
        {
            var tutor = await _tutorRepo.GetTutorByUserIdAsync(userId);

            if(tutor == null)
            {
                return new ResponseApiDTO<ProfileResponseDTO>
                {
                    Success = false,
                    Message = "Hệ thống không ghi nhận bạn là gia sư."
                };
            }

            var profile = _mapper.Map<ProfileResponseDTO>(tutor);
            profile.Id = tutor.TutorId;
            profile.OnlineStatus = tutor.OnlineStatus;
            profile.Status = tutor.Status;

            return new ResponseApiDTO<ProfileResponseDTO>
            {
                Success = true,
                Message = "Đây là hồ sơ gia sư của bạn.",
                Data = profile
            };
        }

        public async Task<ResponseApiDTO> UpdateStatusAsync(int id, string status)
        {
            var tutor = await _tutorRepo.GetByIdAsync(id);

            if(tutor == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Không tìm thấy gia sư nào với Id: {id}."
                };
            }

            tutor.Status = status;
            var result = await _tutorRepo.UpdateTutorAsync(tutor);

            if(!result)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Hệ thống gặp lỗi khi cập nhật trạng thái hồ sơ giảng viên {id}."
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = $"Hệ thống cập nhật trạng thái hồ sơ giảng viên {id} thành công."
            };
        }
    }
}

   