using System.ComponentModel.DataAnnotations;
namespace ETL_web_project.DTOs.Account
{
    public class ResetPasswordDto
    {
        [Required]
        public string Token { get; set; }

        [Required, MinLength(8)]
        public string NewPassword { get; set; }

        [Required, Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}