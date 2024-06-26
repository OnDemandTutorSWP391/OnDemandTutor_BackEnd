using System.ComponentModel.DataAnnotations;

namespace OnDemandTutorApi.DataAccessLayer.Entity
{
    public class RequestDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int RequestCategoryId { get; set; }
        [Required]
        [MaxLength(450)]
        public string UserId { get; set; }
        public string Description { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        /*
            bussiness logic:
            status should be in range: ["", "" ,""] -> disable, enable, on-going,....
            create enum -> define type: -> disable, enable, on-going,....
         */
        public string Status { get; set; } = null!;
    }
}
