using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnDemandTutorApi.DataAccessLayer.Entity
{
    public partial class Rating
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TutorId { get; set; }
        [Required]
        [MaxLength(450)]
        public string UserId { get; set; }
        public float Star { get; set; }
        public string Description { get; set; } = null!;
        //
        public virtual Tutor Tutor { get; set; }
        public virtual User User { get; set; }
    }
}
