using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnDemandTutorApi.DataAccessLayer.Entity
{
    [Table("Level")]
    public class Level
    {
        public Level()
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
