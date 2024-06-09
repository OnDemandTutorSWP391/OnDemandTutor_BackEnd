using OnDemandTutorApi.BusinessLogicLayer.DTO;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface ISubjectLevelService
    {
        public Task<ResponseApiDTO<SubjectLevelDTOWithId>> CreateAsync(string userId, SubjectLevelDTO subjectLevelDTO);
        public Task<ResponseApiDTO<IEnumerable<SubjectLevelDTOWithData>>> GetAllAsync(string? level, string? subject, string? tutor, int page = 1);
        public Task<ResponseApiDTO> UpdateAsync(int id, SubjectLevelDTO subjectLevelDTO);
        public Task<ResponseApiDTO> DeleteAsync(int id);
    }
}
