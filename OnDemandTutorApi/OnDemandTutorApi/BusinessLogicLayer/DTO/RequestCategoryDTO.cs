using System.ComponentModel.DataAnnotations;

namespace OnDemandTutorApi.BusinessLogicLayer.DTO
{
    public class RequestCategoryDTO
    {
        [Required]
        public string CategoryName { get; set; }
    }

    public class RequestCategoryDTOWithId : RequestCategoryDTO
    {
        public int Id { get; set; }
    }
}
