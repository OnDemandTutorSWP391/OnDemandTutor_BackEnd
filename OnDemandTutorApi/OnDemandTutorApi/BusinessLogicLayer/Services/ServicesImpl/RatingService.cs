using AutoMapper;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;
using OnDemandTutorApi.DataAccessLayer.Repositories.RepoImpl;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl
{
    public class RatingService : IRatingService
    {
        private readonly IMapper _mapper;
        private readonly IRatingRepo _ratingRepo;
        private readonly IUserRepo _userRepo;
        private readonly ITutorRepo _tutorRepo;

        public RatingService(IMapper mapper, IRatingRepo ratingRepo, IUserRepo userRepo, ITutorRepo tutorRepo)
        {
            _mapper = mapper;
            _ratingRepo = ratingRepo;
            _userRepo = userRepo;
            _tutorRepo = tutorRepo;
        }
        public async Task<ResponseApiDTO<RatingResponseDTO>> CreateAsync(string userId, RatingRequestDTO ratingRequestDTO)
        {
            var user = await _userRepo.GetByIdAsync(userId);

            var tutor = await _tutorRepo.GetByIdAsync(ratingRequestDTO.TutorId);
            if (tutor == null)
            {
                return new ResponseApiDTO<RatingResponseDTO>
                {
                    Success = false,
                    Message = $"Không tồn tại giảng viên với Id: {ratingRequestDTO.TutorId}"
                };
            }

            var rating = _mapper.Map<Rating>(ratingRequestDTO);
            rating.UserId = userId;
            var result = await _ratingRepo.CreateAysnc(rating);

            if(!result)
            {
                return new ResponseApiDTO<RatingResponseDTO>
                {
                    Success = false,
                    Message = "Hệ thống gặp lỗi khi thêm lượt đánh giá"
                };
            }

            return new ResponseApiDTO<RatingResponseDTO>
            {
                Success = true,
                Message = "Thêm lượt đánh giá thành công.",
                Data = new RatingResponseDTO
                {
                    Id = rating.Id,
                    TutorName = tutor.User.FullName,
                    StudentName = user.FullName,
                    Star = rating.Star,
                    Description = rating.Description,
                }
            };
        }
    }
}
