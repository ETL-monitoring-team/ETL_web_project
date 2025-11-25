using ETL_web_project.Enums;
using System.ComponentModel.DataAnnotations;

namespace ETL_web_project.DTOs
{
    public class ProfileSettingsDto
    {
        public int UserId { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }

        // Şifre doğrulama için (popup'tan geliyor)
        [Required]
        [MinLength(8)]
        public string? ConfirmPassword { get; set; }

        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
    }
}
