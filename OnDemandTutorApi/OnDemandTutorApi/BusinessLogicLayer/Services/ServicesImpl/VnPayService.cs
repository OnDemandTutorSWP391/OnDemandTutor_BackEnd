using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Helper;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using System.Security.Policy;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl
{
    public class VnPayService : IVnPayService
    {
        private readonly VnPayConfiguration _vnPayConfig;

        public VnPayService(VnPayConfiguration vnPayConfig)
        {
            _vnPayConfig = vnPayConfig;
        }

        public string CreatePaymentUrl(HttpContext context, VnPayRequestDTO vnPayRequest)
        {
            vnPayRequest.OrderId = DateTime.Now.Ticks;
            var vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", _vnPayConfig.Version);
            vnpay.AddRequestData("vnp_Command", _vnPayConfig.Command);
            vnpay.AddRequestData("vnp_TmnCode", _vnPayConfig.TmnCode);
            vnpay.AddRequestData("vnp_Amount", (vnPayRequest.Amount * 100).ToString()); //Số tiền thanh toán. Số tiền không 
            //mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND
            //(một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần(khử phần thập phân), sau đó gửi sang VNPAY
            //là: 10000000

            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine(vnPayRequest.Amount);
            Console.ResetColor();

            vnpay.AddRequestData("vnp_CreateDate", vnPayRequest.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", _vnPayConfig.CurrCode);
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(context));
            vnpay.AddRequestData("vnp_Locale", _vnPayConfig.Locale);

            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toán cho đơn hàng: " + vnPayRequest.OrderId);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other
            vnpay.AddRequestData("vnp_ReturnUrl", _vnPayConfig.ReturnUrl);
            vnpay.AddRequestData("vnp_TxnRef", vnPayRequest.OrderId.ToString());

            var paymentUrl = vnpay.CreateRequestUrl(_vnPayConfig.PaymentUrl, _vnPayConfig.HashSecret);

            return paymentUrl;
        }

        public VnPayResponseDTO PaymentExcute(IQueryCollection collection)
        {
            var vnpay = new VnPayLibrary();


            foreach (var (key, value) in collection)
            {
                if(!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, value.ToString());
                }
            }

            var vnp_orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
            var vnp_TransactionId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            var vnp_SecureHash = collection.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value;
            var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            var vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");
            var vnp_Amount = vnpay.GetResponseData("vnp_Amount");

            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _vnPayConfig.HashSecret);

            if(!checkSignature)
            {
                return new VnPayResponseDTO
                {
                    Success = false,
                    Message = "Lỗi xác thực kháo bảo mật VnPay"
                };
            }

            var result = new VnPayResponseDTO
            {
                Success = true,
                PaymentMethod = "VnPay",
                OrderDescription = vnp_OrderInfo,
                OrderId = vnp_orderId.ToString(),
                TransactionId = vnp_TransactionId.ToString(),
                Token = vnp_SecureHash,
                VnPayResponseCode = vnp_ResponseCode,
                Amount = float.Parse(vnp_Amount),
            };

            switch(result.VnPayResponseCode)
            {
                case "00":
                    result.Message = "Giao dịch thành công";
                    break;
                case "07":
                    result.Message = "Trừ tiền thành công. Giao dịch bị nghi ngờ (liên quan tới lừa đảo, giao dịch bất thường).";
                    break;
                case "09":
                    result.Message = "Giao dịch không thành công do: Thẻ/Tài khoản của khách hàng chưa đăng ký dịch vụ InternetBanking tại ngân hàng.";
                    break;
                case "10":
                    result.Message = "Giao dịch không thành công do: Khách hàng xác thực thông tin thẻ/tài khoản không đúng quá 3 lần";
                    break;
                case "11":
                    result.Message = "Giao dịch không thành công do: Đã hết hạn chờ thanh toán. Xin quý khách vui lòng thực hiện lại giao dịch.";
                    break;
                case "12":
                    result.Message = "Giao dịch không thành công do: Thẻ/Tài khoản của khách hàng bị khóa.";
                    break;
                case "13":
                    result.Message = "Giao dịch không thành công do Quý khách nhập sai mật khẩu xác thực giao dịch (OTP). Xin quý khách vui lòng thực hiện lại giao dịch.";
                    break;
                case "24":
                    result.Message = "Giao dịch không thành công do: Khách hàng hủy giao dịch";
                    break;
                case "51":
                    result.Message = "Giao dịch không thành công do: Tài khoản của quý khách không đủ số dư để thực hiện giao dịch.";
                    break;
                case "65":
                    result.Message = "Giao dịch không thành công do: Tài khoản của Quý khách đã vượt quá hạn mức giao dịch trong ngày.";
                    break;
                case "75":
                    result.Message = "Ngân hàng thanh toán đang bảo trì.";
                    break;
                case "79":
                    result.Message = "Giao dịch không thành công do: KH nhập sai mật khẩu thanh toán quá số lần quy định. Xin quý khách vui lòng thực hiện lại giao dịch";
                    break;
                case "99":
                    result.Message = "Các lỗi khác (lỗi còn lại, không có trong danh sách mã lỗi đã liệt kê)";
                    break;
            }

            return result;
        }
    }
}
