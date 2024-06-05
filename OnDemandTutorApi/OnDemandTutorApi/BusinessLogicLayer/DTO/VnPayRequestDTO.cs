namespace OnDemandTutorApi.BusinessLogicLayer.DTO
{
    public class VnPayRequestDTO
    {
        public long OrderId { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public float Amount { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
