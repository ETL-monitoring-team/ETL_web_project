using ETL_web_project.DTOs;

namespace ETL_web_project.Models
{
    public class SettingsViewModel
    {
        public ProfileSettingsDto Profile { get; set; } = new();
        public ChangePasswordDto PasswordModel { get; set; } = new();
    }
}
