using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnDemandTutorApi.DataAccessLayer.Entity
{
    public partial class User : IdentityUser
    {
        public User()
        {
            Requests = new List<Request>();
            CoinManagements = new List<CoinManagement>();
            RefreshTokens = new List<RefreshToken>();
            StudentJoins = new List<StudentJoin>();
            Ratings = new List<Rating>();
        }
        public string? FullName { get; set; } = null!;
        public string? IdentityCard { get; set; } = null!;
        public DateTime Dob { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool Status { get; set; }
        public string Gender { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        //
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual Tutor Tutor { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<CoinManagement> CoinManagements { get; set; }
        public virtual ICollection<StudentJoin> StudentJoins { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }

}
