using System.ComponentModel.DataAnnotations;

namespace OnDemandTutorApi.BusinessLogicLayer.DTO
{
    public class UserDTO
    {
        [Required]
        public string? FullName { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string ConfirmedPassword { get; set; } = null!;
        [Required]
        public string? IdentityCard { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
                ApplyFormatInEditMode = true)]
        public DateTime? Dob { get; set; }
        public string? Gender { get; set; } = null!;
        public string? Avatar { get; set; } = null!;

        [Required]
        public string Role { get; set; } = null!;
    }

    public class UserRequestDTO
    {
        [Required]
        public string? FullName { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string ConfirmedPassword { get; set; } = null!;
        [Required]
        public string? IdentityCard { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
                ApplyFormatInEditMode = true)]
        public DateTime? Dob { get; set; }
        [Required]
        public string Phone {  get; set; } = null!;
        public string? Gender { get; set; } = null!;
        public string? Avatar { get; set; } = null!;

        [Required]
        public string Role { get; set; } = null!;
    }

    public class UserAuthenDTO
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }

    public class UserResetPassDTO
    {
        public string? Email { get; set; }
    }
    public class UserProfileUpdateDTO
    {
        public string? FullName { get; set; } = null!;
        public DateTime Dob { get; set; }
        public string Gender { get; set; } = null!;
        public string Avatar { get; set; } = null!;
    }
}
