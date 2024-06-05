using OnDemandTutorApi.BusinessLogicLayer.DTO;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface IEmailService
    {
        public ResponseDTO SendEmail(EmailDTO emailDTO);
       
    }
}
