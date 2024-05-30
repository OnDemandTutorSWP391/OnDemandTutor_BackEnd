using AutoMapper;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl
{
    public class TutorService : ITutorService
    {
        private readonly ITutorRepo _tutorRepo;
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;

        public TutorService(ITutorRepo tutorRepo, IMapper mapper, IUserRepo userRepo)
        {
            _tutorRepo = tutorRepo;
            _mapper = mapper;
            _userRepo = userRepo;
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
    }
}

   