using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnDemandTutorApi.DataAccessLayer.Entity
{
    [Table("StudentJoin")]
    public class StudentJoin
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(450)]
        public string UserId { get; set; }
        [Required]
        public int TimeId { get; set; }
    }
}
