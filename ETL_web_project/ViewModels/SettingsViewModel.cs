using ETL_web_project.DTOs.Account;

namespace ETL_web_project.ViewModels
{
    public class SettingsViewModel
    {
        public ProfileSettingsDto Profile { get; set; } = new();
        public ChangePasswordDto PasswordModel { get; set; } = new();
    }
}
