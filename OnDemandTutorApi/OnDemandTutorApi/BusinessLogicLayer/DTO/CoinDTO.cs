using System.ComponentModel.DataAnnotations;

namespace OnDemandTutorApi.BusinessLogicLayer.DTO
{
    #region Request
    public class CoinDTO
    {
        [Required]
        [MaxLength(450)]
        public string UserId { get; set; }
        [Required]
        public float Coin { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
    }
    #endregion

    #region Response
    public class CoinDTOWithId : CoinDTO
    {   
        public int Id { get; set; }
    }

    public class CoinResponseDTO
    {
        public string FullName { get; set; }
        public float Coin { get; set; }
        public DateTime Date { get; set; }
    }

    public class CoinTransferResponseDTO
    {
        public int Id { get; set; }
        public string SenderId { get; set; } = null!;
        public string SenderName { get; set; } = null!;
        public string ReceiverId { get; set; } = null!;
        public string ReceiverName { get; set; } = null !;
        public float Coin { get; set; }
        public DateTime Date { get; set; }
    }
    #endregion
}
