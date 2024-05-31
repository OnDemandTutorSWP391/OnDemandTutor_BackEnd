using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnDemandTutorApi.DataAccessLayer.Entity
{
    [Table("Request")]
    public class Request
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int RequestCategoryId { get; set; }
        [Required]
        [MaxLength(450)]
        public string UserId { get; set; }
        public string Description { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; } = null!;
        
    }
}
