using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnDemandTutorApi.DataAccessLayer.Entity
{
    [Table("Subject")]
    public class Subject
    {
        public Subject()
        {
            SubjectLevels = new List<SubjectLevel>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        //
        public virtual ICollection<SubjectLevel> SubjectLevels { get; set; }
    }
}
