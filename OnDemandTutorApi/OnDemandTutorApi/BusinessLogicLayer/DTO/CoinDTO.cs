﻿using System.ComponentModel.DataAnnotations;

namespace OnDemandTutorApi.BusinessLogicLayer.DTO
{
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

    public class CoinDTOWithId : CoinDTO
    {   
        public int Id { get; set; }
    }
}