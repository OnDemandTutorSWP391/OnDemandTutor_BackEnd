using OnDemandTutorApi.BusinessLogicLayer.DTO;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(HttpContext context, VnPayRequestDTO vnPayRequest);
        ResponseApiDTO<VnPayResponseDTO> PaymentExcute(IQueryCollection collection);
    }
}
