using ETL_web_project.DTOs.Account;
using ETL_web_project.ViewModels;

public interface ISettingsService
{
    Task<SettingsViewModel> GetSettingsForUserAsync(int userId);
    Task<ProfileUpdateResult> UpdateProfileAsync(ProfileSettingsDto profileDto);
    Task<ProfileUpdateResult> ChangePasswordAsync(ChangePasswordDto dto);
}
