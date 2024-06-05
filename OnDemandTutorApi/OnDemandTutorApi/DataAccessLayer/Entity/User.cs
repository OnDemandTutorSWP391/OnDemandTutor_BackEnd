using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnDemandTutorApi.DataAccessLayer.Entity
{
    [Table("User")]
    public class User : IdentityUser
    {
        public string? FullName { get; set; } = null!;
        public string? IdentityCard { get; set; } = null!;
        public DateTime Dob { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        public string Gender { get; set; } = null!;
        public string Avatar { get; set; } = null!;
    }

}
