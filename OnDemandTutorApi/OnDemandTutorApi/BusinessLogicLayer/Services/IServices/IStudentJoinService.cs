using OnDemandTutorApi.BusinessLogicLayer.DTO;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface IStudentJoinService
    {
        public Task<ResponseApiDTO<StudentJoinDTOWithId>> CreateAsync(StudentJoinDTO studentJoinDTO);
    }
}
