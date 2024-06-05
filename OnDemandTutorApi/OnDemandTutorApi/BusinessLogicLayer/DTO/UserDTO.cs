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

        [Required(ErrorMessage = "Email is required"), EmailAddress]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "ConfirmedPassword is required")]
        [Compare("Password", ErrorMessage = "The confirmed password does not match password")]
        public string ConfirmedPassword { get; set; } = null!;

        [Required]
        public string? IdentityCard { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
                ApplyFormatInEditMode = true)]
        public DateTime? Dob { get; set; }

        [Required]
        public string PhoneNumber {  get; set; } = null!;

        public string? Gender { get; set; } = null!;

        public string? Avatar { get; set; } = null!;

        [Required]
        public string Role { get; set; } = null!;
    }

    public class UserAuthenDTO
    {
        [Required, EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }

    public class UserForgotPassDTO
    {
        public string? Email { get; set; }
    }

    public class UserResetPassDTO
    {
        [Required]
        public string NewPassword { get; set; } = null!;

        [Required]
        [Compare("NewPassword", ErrorMessage = "Confirmed New Password does not match New Password.")]
        public string ConfirmedNewPassword { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
    }

    public class UserGetProfileDTO
    {
        public string? FullName { get; set; } = null!;
        public string Email { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
                ApplyFormatInEditMode = true)]
        public DateTime Dob { get; set; }
        public string Gender { get; set; } = null!;
        public string Avatar { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
                ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
    }

    public class UserProfileUpdateDTO
    {
        public string? FullName { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
                ApplyFormatInEditMode = true)]
        public DateTime Dob { get; set; }
        public string Gender { get; set; } = null!;
        public string Avatar { get; set; } = null!;

    }

    public class UserUpdateDTO
    {
        [Required]
        public string? FullName { get; set; } = null!;

        [Required(ErrorMessage = "Email is required"), EmailAddress]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "ConfirmedPassword is required")]
        [Compare("Password", ErrorMessage = "The confirmed password does not match password")]
        public string ConfirmedPassword { get; set; } = null!;

        [Required]
        public string? IdentityCard { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
                ApplyFormatInEditMode = true)]
        public DateTime? Dob { get; set; }

        [Required]
        public string PhoneNumber { get; set; } = null!;

        public string? Gender { get; set; } = null!;

        public string? Avatar { get; set; } = null!;
    }

    public class UserResponseDTO
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string? FullName { get; set; } = null!;

        [Required(ErrorMessage = "Email is required"), EmailAddress]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        public string PasswordHash { get; set; } = null!;

        [Required]
        public string? IdentityCard { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
                ApplyFormatInEditMode = true)]
        public DateTime? Dob { get; set; }

        [Required]
        public string PhoneNumber { get; set; } = null!;

        public string? Gender { get; set; } = null!;

        public string? Avatar { get; set; } = null!;

        [Required]
        public IList<string> Roles { get; set; } = null!;
    }
}
