using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnDemandTutorApi.DataAccessLayer.Entity
{
    public partial class RefreshToken
    {
        [Key]
        public string Id { get; set; }
        [MaxLength(450)]
        public string UserId { get; set; }
        public string JwtId { get; set; }
        public string Token { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
        //
        public virtual User User { get; set; }
    }
}
