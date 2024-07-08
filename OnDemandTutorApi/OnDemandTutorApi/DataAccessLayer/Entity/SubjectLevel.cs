using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace OnDemandTutorApi.DataAccessLayer.Entity
{
    public partial class SubjectLevel
    {
        public SubjectLevel()
        {
            StudentJoins = new List<StudentJoin>();
            Times = new List<Time>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public int LevelId { get; set; }
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public int TutorId { get; set; }
        public string Name { get; set; }
        [Required]
        public string Description { get; set; } = null!;
        [Required] 
        public string Url { get; set; } = null!;
        [Required]
        public float Coin { get; set; }
        public int LimitMember { get; set; }
        [AllowNull]
        public string? Image {  get; set; }
        //
        public virtual Level Level { get; set; } = null!;
        public virtual Subject Subject { get; set; } = null!;
        public virtual Tutor Tutor { get; set; } = null!;
        public virtual ICollection<StudentJoin> StudentJoins { get; set; } = null!;
        public virtual ICollection<Time> Times { get; set; } = null!;
    }
}
