using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace OnDemandTutorApi.DataAccessLayer.Entity
{
    public partial class Time
    {
        [Key]
        public int Id {  get; set; }
        [Required]
        public int SubjectLevelId { get; set; }
        [Required]
        public string SlotName { get; set; } = null!;
        [Required]
        public DateTime StartSlot { get; set; }
        [Required]
        public DateTime EndSlot { get; set; }
        [Required]
        public DateTime Date { get; set; }
        //
        public virtual SubjectLevel SubjectLevel { get; set; }
    }
}
