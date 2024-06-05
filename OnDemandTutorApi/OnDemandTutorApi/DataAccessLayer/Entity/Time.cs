using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnDemandTutorApi.DataAccessLayer.Entity
{
    [Table("Time")]
    public class Time
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TutorId { get; set; }
        [Required]
        public DateTime StartSlot { get; set; }
        [Required]
        public DateTime EndSlot { get; set; }
        [Required]
        public int SubjectLevelId { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public float Coin { get; set; }
    }
}
