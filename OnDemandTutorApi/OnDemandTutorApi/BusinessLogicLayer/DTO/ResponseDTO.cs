namespace OnDemandTutorApi.BusinessLogicLayer.DTO
{
    public class ResponseDTO
    {
        public string Message { get; set; } = null!;
        public int StatusCode {  get; set; }
    }

    public class ResponseDTO<T> : ResponseDTO
    {
        public T Data { get; set; }
    }
}
