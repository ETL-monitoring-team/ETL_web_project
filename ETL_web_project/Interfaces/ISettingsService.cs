using ETL_web_project.DTOs;
using ETL_web_project.Models;

public interface ISettingsService
{
    Task<SettingsViewModel> GetSettingsForUserAsync(int userId);

    Task<ProfileUpdateResult> UpdateProfileAsync(ProfileSettingsDto profileDto);

    Task<ProfileUpdateResult> ChangePasswordAsync(ChangePasswordDto dto);

    Task<ProfileUpdateResult> UpdatePreferencesAsync(UserPreferenceDto prefs);   
}
