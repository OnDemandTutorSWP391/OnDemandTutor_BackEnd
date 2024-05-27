namespace OnDemandTutorApi.BusinessLogicLayer.DTO
{
    public class ResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
    }

    public class ResponseDTO<T> : ResponseDTO
    {
        public T Data { get; set; }
    }
}
