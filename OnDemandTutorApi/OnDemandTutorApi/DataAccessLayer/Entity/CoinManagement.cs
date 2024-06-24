using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnDemandTutorApi.DataAccessLayer.Entity
{
    public partial class CoinManagement
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(450)]
        public string UserId { get; set; }
        public float Coin {  get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public long TransactionId { get; set; }
        //
        public virtual User User { get; set; } = null!;
    }
}
