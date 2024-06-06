using System.ComponentModel.DataAnnotations;

namespace OnDemandTutorApi.BusinessLogicLayer.DTO
{
    public class ResponseDTO
    {
        public string Description { get; set; } = null!;
    }

    public class ResponseContentDTO
    {
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Content { get; set; } = null!;
    }

    public class ResponseDTOWithData : ResponseDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int RequestId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string FullName { get; set; }
        public DateTime ResponseDate { get; set; }
    }
}
