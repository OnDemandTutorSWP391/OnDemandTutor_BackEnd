using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnDemandTutorApi.DataAccessLayer.Entity
{
    [Table("RequestCategory")]
    public class RequestCategory
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string CategoryName { get; set; }
    }
}
