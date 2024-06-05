using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnDemandTutorApi.DataAccessLayer.Entity
{
    [Table("Rating")]
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TutorId { get; set; }
        public float Star { get; set; }
        public string Description { get; set; } = null!;
        [Required]
        [MaxLength(450)]
        public string UserId { get; set; }
    }
}
