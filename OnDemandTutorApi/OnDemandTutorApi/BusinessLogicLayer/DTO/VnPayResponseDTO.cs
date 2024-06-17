namespace OnDemandTutorApi.BusinessLogicLayer.DTO
{
    public class VnPayResponseDTO
    {
        public string PaymentMethod { get; set; }
        public string OrderDescription { get; set; }
        public string OrderId { get; set; }
        public string TransactionId { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
        public float Amount { get; set; }
    }
}
