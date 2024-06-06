using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnDemandTutorApi.DataAccessLayer.Entity
{
    [Table("RequestCategory")]
    public class RequestCategory
    {
        public RequestCategory()
        {
            Requests = new List<Request>();
        }
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string CategoryName { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
