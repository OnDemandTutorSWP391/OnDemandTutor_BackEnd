using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnDemandTutorApi.DataAccessLayer.Entity
{
    [Table("CoinManagement")]
    public class CoinManagement
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(450)]
        public string UserId { get; set; }
        public float Coin {  get; set; }
        public DateTime Date { get; set; }
    }
}
