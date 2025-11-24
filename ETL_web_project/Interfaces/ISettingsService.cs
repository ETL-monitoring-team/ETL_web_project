using ETL_web_project.DTOs;
using ETL_web_project.Models;

namespace ETL_web_project.Interfaces
{
    public interface ISettingsService
    {
        Task<SettingsViewModel> GetSettingsForUserAsync(int userId);

        Task<bool> UpdateProfileAsync(ProfileSettingsDto dto);

        Task<bool> ChangePasswordAsync(ChangePasswordDto dto);

        Task UpdatePreferencesAsync(int userId, UserPreferenceDto dto);
    }
}
