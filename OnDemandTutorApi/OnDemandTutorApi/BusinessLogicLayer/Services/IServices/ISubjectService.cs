using OnDemandTutorApi.BusinessLogicLayer.DTO;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface ISubjectService
    {
        public Task<ResponseApiDTO> CreateAsync(SubjectDTO subjectDTO);
        public Task<ResponseApiDTO<IEnumerable<SubjectDTOWithId>>> GetAllAsync(string? search, string? subjectId, string? sortBy, int page = 1);
        public Task<ResponseApiDTO> UpdateAsync(int id, SubjectDTO subjectDTO);
        public Task<ResponseApiDTO> DeleteAsync(int id);
    }
}
