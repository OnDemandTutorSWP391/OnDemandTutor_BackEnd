using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnDemandTutorApi.DataAccessLayer.Entity
{
    public partial class Request
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
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Chờ xử lý";
        //
        public virtual RequestCategory Category { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual Response Response { get; set; } = null!;
    }
}
