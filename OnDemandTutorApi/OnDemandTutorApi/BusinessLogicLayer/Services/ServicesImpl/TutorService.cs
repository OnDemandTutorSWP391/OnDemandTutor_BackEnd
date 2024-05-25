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

        public TutorService(ITutorRepo tutorRepo, IMapper mapper)
        {
            _tutorRepo = tutorRepo;
            _mapper = mapper;
        }
        public async Task<int> AddTutorAsync(TutorDTO tutorDTO)
        {
            var tutor = _mapper.Map<Tutor>(tutorDTO);
            return await _tutorRepo.AddTutorAsync(tutor);
        }
    }
}
