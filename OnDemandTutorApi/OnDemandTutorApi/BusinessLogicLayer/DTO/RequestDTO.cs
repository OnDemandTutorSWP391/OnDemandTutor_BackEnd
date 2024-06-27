using System.ComponentModel.DataAnnotations;

namespace OnDemandTutorApi.BusinessLogicLayer.DTO
{
    public class RequestDTO
    {
        public int RequestCategoryId { get; set; }
        public string Description { get; set; } = null!;
    }

    public class RequestDTOWithUserData : RequestDTO
    {
        [Required]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
    }

    public class RequestDTOWithData : RequestDTOWithUserData
    {
        [Required]
        public string UserId {  get; set; }
        public bool IsLocked { get; set; }
    }

    public class RequestUpdateStatusDTO
    {
        public string Status { get; set; }
    }
}
