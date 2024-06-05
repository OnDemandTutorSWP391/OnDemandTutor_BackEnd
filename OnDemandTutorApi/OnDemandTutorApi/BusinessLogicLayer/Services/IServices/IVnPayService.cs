using OnDemandTutorApi.BusinessLogicLayer.DTO;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(HttpContext context, VnPayRequestDTO vnPayRequest);
        VnPayResponseDTO PaymentExcute(IQueryCollection collection);
    }
}
