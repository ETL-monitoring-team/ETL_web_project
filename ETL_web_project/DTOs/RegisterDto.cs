using System.ComponentModel.DataAnnotations;

namespace ETL_web_project.DTOs
{
    public class RegisterDto
    {
        [Required]
        [MinLength(8)]
        [MaxLength(100)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = null!;

        [Required]
        [MinLength(8)]
        [MaxLength(100)]
        [Display(Name = "Username")]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        [Display(Name = "Work Email")]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
