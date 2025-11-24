using System;
using System.ComponentModel.DataAnnotations;
using ETL_web_project.Enums;

namespace ETL_web_project.DTOs
{
    public class ProfileSettingsDto
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(100, ErrorMessage = "Username cannot be longer than 100 characters.")]
        [Display(Name = "Username")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Work email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid work email address.")]
        [MaxLength(255, ErrorMessage = "Work email cannot be longer than 255 characters.")]
        [Display(Name = "Work Email")]
        public string Email { get; set; } = null!;

        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
    }
}
