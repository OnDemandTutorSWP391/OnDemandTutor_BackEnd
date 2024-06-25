using AutoMapper;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Helper;
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

        public static int PAGE_SIZE { get; set; } = 5;

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
                return tutor.Id;
            }
            else
            {
                // Người dùng đã là tutor, không thêm tutor mới
                return existingTutor.Id; // Trả về ID của tutor hiện tại
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

            var category = await _categoryRepo.GetByNameAsync("Rental Service Support");

            var requestDTO = new RequestDTO
            {
                RequestCategoryId = category.Id,
                Description = $"Yêu cầu phê duyệt dịch vụ giảng viên với TutorId: {tutor.Id}."
            };

           
            var request = _mapper.Map<Request>(requestDTO);
            request.UserId = userId;

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
            profile.Id = tutor.Id;
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

        public async Task<ResponseApiDTO<IEnumerable<TutorResponseDTO>>> GetAllTutorsForStudentAsync(string? seacrch, string? sortBy, int page = 1)
        {
            var tutors = await _tutorRepo.GetTutorsAsync();
            tutors = tutors.Where(x => x.User.IsLocked == false);

            tutors = tutors.Where(x => x.Status == "Chấp thuận");

            if (!string.IsNullOrEmpty(seacrch))
            {
                tutors = tutors.Where(x => x.User.FullName.IndexOf(seacrch, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            tutors = tutors.OrderBy(x => x.User.FullName);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch(sortBy)
                {
                    case "des": tutors = tutors.OrderByDescending(x => x.User.FullName); break;
                }
            }

            var result = PaginatedList<Tutor>.Create(tutors, page, PAGE_SIZE);

            return new ResponseApiDTO<IEnumerable<TutorResponseDTO>>
            {
                Success = true,
                Message = "Danh sách các gia sư",
                Data = result.Select(x => new TutorResponseDTO
                {
                    TutorName = x.User.FullName,
                    AcademicLevel = x.AcademicLevel,
                    WorkPlace = x.WorkPlace,
                    Degree = x.Degree,
                    TutorServiceName = x.TutorServiceName,
                    TutorServiceDescription = x.TutorServiceDescription,
                    TutorServiceVideo = x.TutorServiceVideo,
                    LearningMaterialDemo = x.LearningMaterialDemo,
                    Status = x.Status,
                    OnlineStatus = x.OnlineStatus,
                    AverageStar = x.Ratings.Any() ? x.Ratings.Average(r => r.Star) : 0,
                    IsLocked = x.User.IsLocked,
                })
            };
        }

        public async Task<ResponseApiDTO<IEnumerable<TutorResponseDTO>>> GetAllTutorsAsync(string? seacrch, string? sortBy, int page = 1)
        {
            var tutors = await _tutorRepo.GetTutorsAsync();

            if (!string.IsNullOrEmpty(seacrch))
            {
                tutors = tutors.Where(x => (x.User.FullName.IndexOf(seacrch, StringComparison.OrdinalIgnoreCase) >= 0) 
                                            || x.Id == Convert.ToInt32(seacrch));
            }

            tutors = tutors.OrderBy(x => x.User.FullName);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "des": tutors = tutors.OrderByDescending(x => x.User.FullName); break;
                }
            }

            var result = PaginatedList<Tutor>.Create(tutors, page, PAGE_SIZE);

            return new ResponseApiDTO<IEnumerable<TutorResponseDTO>>
            {
                Success = true,
                Message = "Danh sách các gia sư",
                Data = result.Select(x => new TutorResponseDTO
                {
                    TutorName = x.User.FullName,
                    AcademicLevel = x.AcademicLevel,
                    WorkPlace = x.WorkPlace,
                    Degree = x.Degree,
                    TutorServiceName = x.TutorServiceName,
                    TutorServiceDescription = x.TutorServiceDescription,
                    TutorServiceVideo = x.TutorServiceVideo,
                    LearningMaterialDemo = x.LearningMaterialDemo,
                    Status = x.Status,
                    OnlineStatus = x.OnlineStatus,
                    AverageStar = x.Ratings.Any() ? x.Ratings.Average(r => r.Star) : 0,
                    IsLocked = x.User.IsLocked,
                })
            };
        }
    }
}

   