namespace OnDemandTutorApi.BusinessLogicLayer.DTO
{
    public class ResponseApiDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
    }

    public class ResponseApiDTO<T> : ResponseApiDTO
    {
        public T Data { get; set; }
    }
}
