using AutoMapper;
using Mailjet.Client.Resources;
using Microsoft.IdentityModel.Tokens;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Helper;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;
using OnDemandTutorApi.DataAccessLayer.Repositories.RepoImpl;
using System.Drawing.Printing;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl
{
    public class RatingService : IRatingService
    {
        private readonly IMapper _mapper;
        private readonly IRatingRepo _ratingRepo;
        private readonly IUserRepo _userRepo;
        private readonly ITutorRepo _tutorRepo;

        public static int PAGE_SIZE { get; set; } = 5;

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

        public async Task<ResponseApiDTO<IEnumerable<RatingResponseDTO>>> GetAllByTutorIdAsync(string tutorId, string? sortBy, int page = 1)
        {
            var ratings = await _ratingRepo.GetAllByTutorIdAsync(Convert.ToInt32(tutorId));

            ratings = ratings.OrderByDescending(x => x.Star);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "incre": ratings = ratings.OrderBy(x => x.Star); break;
                }
            }

            var result = PaginatedList<Rating>.Create(ratings, page, PAGE_SIZE);
            if (result.IsNullOrEmpty())
            {
                return new ResponseApiDTO<IEnumerable<RatingResponseDTO>>
                {
                    Success = true,
                    Message = "Hiện tại chưa có học sinh nào đánh giá bạn."
                };
            }

            return new ResponseApiDTO<IEnumerable<RatingResponseDTO>>
            {
                Success = true,
                Message = "Đây là danh sách các đánh giá của bạn",
                Data = result.Select(x => new RatingResponseDTO
                {
                    Id = x.Id,
                    TutorName = x.Tutor.User.FullName,
                    StudentName = x.User.FullName,
                    Star = x.Star,
                    Description = x.Description,
                })
            };
        }

        public async Task<ResponseApiDTO<IEnumerable<RatingResponseDTO>>> GetAllByTutorSelfAsync(string userId, string? sortBy, int page = 1)
        {
            var tutor = await _tutorRepo.GetTutorByUserIdAsync(userId);

            var ratings = await _ratingRepo.GetAllByTutorIdAsync(Convert.ToInt32(tutor.Id));

            ratings = ratings.OrderByDescending(x => x.Star);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "incre": ratings = ratings.OrderBy(x => x.Star); break;
                }
            }

            var result = PaginatedList<Rating>.Create(ratings, page, PAGE_SIZE);
            if (result.IsNullOrEmpty())
            {
                return new ResponseApiDTO<IEnumerable<RatingResponseDTO>>
                {
                    Success = true,
                    Message = "Hiện tại chưa có học sinh nào đánh giá bạn."
                };
            }

            return new ResponseApiDTO<IEnumerable<RatingResponseDTO>>
            {
                Success = true,
                Message = "Đây là danh sách các đánh giá của bạn",
                Data = result.Select(x => new RatingResponseDTO
                {
                    Id = x.Id,
                    TutorName = x.Tutor.User.FullName,
                    StudentName = x.User.FullName,
                    Star = x.Star,
                    Description = x.Description,
                })
            };
        }

        public async Task<ResponseApiDTO> UpdateAsync(int ratingId, string userId, RatingUpdateDTO ratingUpdateDTO)
        {
            var rating = await _ratingRepo.GetByIdAsync(ratingId);

            if (rating == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Lỗi không tìm thấy đánh giá nào với Id: {ratingId}"
                };
            }

            if (rating.UserId != userId)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Bạn không thể xóa đánh giá của người dùng khác."
                };
            }

            var update = _mapper.Map<Rating>(ratingUpdateDTO);
            var result = await _ratingRepo.UpdateAsync(update);

            if(!result)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Hệ thống gặp lỗi trong quá trình chỉnh sửa đánh giá của bạn."
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = $"Chỉnh sửa đánh giá thành công."
            };
        }

        public async Task<ResponseApiDTO<IEnumerable<RatingResponseDTO>>> GetAllAsync(string? userId, string? tutorId, string? sortBy, int page = 1)
        {
            var ratings = await _ratingRepo.GetAllAsync();

            if(!string.IsNullOrEmpty(userId))
            {
                ratings = ratings.Where(x => x.UserId == userId);
            }

            if (!string.IsNullOrEmpty(tutorId))
            {
                ratings = ratings.Where(x => x.TutorId == Convert.ToInt32(tutorId));
            }

            ratings = ratings.OrderByDescending(x => x.Star);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "incre": ratings = ratings.OrderBy(x => x.Star); break;
                }
            }

            var result = PaginatedList<Rating>.Create(ratings, page, PAGE_SIZE);
            if (result.IsNullOrEmpty())
            {
                return new ResponseApiDTO<IEnumerable<RatingResponseDTO>>
                {
                    Success = true,
                    Message = "Hiện tại chưa có học sinh nào đánh giá bạn."
                };
            }

            return new ResponseApiDTO<IEnumerable<RatingResponseDTO>>
            {
                Success = true,
                Message = "Đây là danh sách các đánh giá của bạn",
                Data = result.Select(x => new RatingResponseDTO
                {
                    Id = x.Id,
                    TutorName = x.Tutor.User.FullName,
                    StudentName = x.User.FullName,
                    Star = x.Star,
                    Description = x.Description,
                })
            };
        }

        public async Task<ResponseApiDTO> DeleteForStudentAsync(int ratingId, string userId)
        {
            var rating = await _ratingRepo.GetByIdAsync(ratingId);
            if (rating == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Không tồn tại đánh giá trong hệ thống."
                };
            }

            if(rating.UserId != userId)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Bạn không thể xóa đánh giá của người dùng khác."
                };
            }

            var result = await _ratingRepo.DeleteAsync(rating);

            if(!result)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Lỗi xảy ra khi cố xóa đánh giá."
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = "Xóa đánh giá thành công."
            };
        }

        public async Task<ResponseApiDTO> DeleteAsync(int ratingId)
        {
            var rating = await _ratingRepo.GetByIdAsync(ratingId);
            if (rating == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Không tồn tại đánh giá trong hệ thống."
                };
            }

            var result = await _ratingRepo.DeleteAsync(rating);

            if (!result)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Lỗi xảy ra khi cố xóa đánh giá."
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = "Xóa đánh giá thành công."
            };
        }
    }
}
