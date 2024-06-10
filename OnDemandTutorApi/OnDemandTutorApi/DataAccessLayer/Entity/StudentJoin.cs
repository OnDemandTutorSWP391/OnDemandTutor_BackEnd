using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnDemandTutorApi.DataAccessLayer.Entity
{
    public partial class StudentJoin
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(450)]
        public string UserId { get; set; }
        [Required]
        public int SubjectLevelId { get; set; }
        //
        public virtual User User { get; set; }
        public virtual SubjectLevel SubjectLevel { get; set; }
    }
}
