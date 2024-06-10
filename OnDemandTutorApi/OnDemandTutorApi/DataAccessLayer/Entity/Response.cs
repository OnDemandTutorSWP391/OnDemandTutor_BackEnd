using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnDemandTutorApi.DataAccessLayer.Entity
{
    public partial class Response
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int RequestId {  get; set; }
        public string Description { get; set; } = null!;
        public DateTime ResponseDate { get; set; } = DateTime.Now;
        //
        public virtual Request Request { get; set; } = null!;
    }
}
