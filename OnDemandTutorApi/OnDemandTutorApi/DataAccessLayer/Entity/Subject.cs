using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnDemandTutorApi.DataAccessLayer.Entity
{
    [Table("Subject")]
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
