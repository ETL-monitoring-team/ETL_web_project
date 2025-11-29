using System.ComponentModel.DataAnnotations;
namespace ETL_web_project.DTOs.Account
{
    public class ChangePasswordDto
    {
        public int UserId { get; set; }

        [Required]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}