using ETL_web_project.Data.Context;
using ETL_web_project.Data.Entities;
using ETL_web_project.DTOs;
using ETL_web_project.Handlers;
using ETL_web_project.Interfaces;
using ETL_web_project.Models;
using Microsoft.EntityFrameworkCore;

namespace ETL_web_project.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly ProjectContext _context;

        public SettingsService(ProjectContext ctx)
        {
            _context = ctx;
        }

        // =============== LOAD SETTINGS ===============
        public async Task<SettingsViewModel> GetSettingsForUserAsync(int userId)
        {
            var user = await _context.UserAccounts
                .AsNoTracking()
                .FirstAsync(u => u.UserId == userId);

            return new SettingsViewModel
            {
                Profile = new ProfileSettingsDto
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Email = user.Email,
                    Role = user.Role,
                    CreatedAt = user.CreatedAt,
                    LastLoginAt = user.LastLoginAt
                },
                PasswordModel = new ChangePasswordDto { UserId = user.UserId },
                Preferences = new UserPreferenceDto()
            };
        }

        // =============== UPDATE PROFILE ===============
        public async Task<ProfileUpdateResult> UpdateProfileAsync(ProfileSettingsDto dto)
        {
            var user = await _context.UserAccounts
                .FirstOrDefaultAsync(u => u.UserId == dto.UserId);

            if (user == null)
                return new ProfileUpdateResult(false, "User not found.");

            // PASSWORD VERIFY
            if (!PasswordHashHandler.VerifyPassword(dto.ConfirmPassword!, user.PasswordHash))
                return new ProfileUpdateResult(false, "Password is incorrect.");

            // USERNAME RULES
            if (dto.Username.Length < 8)
                return new ProfileUpdateResult(false, "Username must be at least 8 characters.");

            if (dto.Username.Length > 100)
                return new ProfileUpdateResult(false, "Username cannot exceed 100 characters.");

            // EMAIL RULES
            if (dto.Email.Length > 255)
                return new ProfileUpdateResult(false, "Email cannot exceed 255 characters.");

            // UNIQUE CHECKS
            if (await _context.UserAccounts.AnyAsync(u => u.UserId != dto.UserId && u.Username == dto.Username))
                return new ProfileUpdateResult(false, "This username is already in use.");

            if (await _context.UserAccounts.AnyAsync(u => u.UserId != dto.UserId && u.Email == dto.Email))
                return new ProfileUpdateResult(false, "This email is already in use.");

            // UPDATE
            user.Username = dto.Username;
            user.Email = dto.Email;

            await _context.SaveChangesAsync();
            return new ProfileUpdateResult(true);
        }

        // =============== CHANGE PASSWORD ===============
        public async Task<ProfileUpdateResult> ChangePasswordAsync(ChangePasswordDto dto)
        {
            var user = await _context.UserAccounts
                .FirstOrDefaultAsync(u => u.UserId == dto.UserId);

            if (user == null)
                return new ProfileUpdateResult(false, "User not found.");

            // Check current password
            if (!PasswordHashHandler.VerifyPassword(dto.CurrentPassword, user.PasswordHash))
                return new ProfileUpdateResult(false, "Current password is incorrect.");

            // New password rules
            if (dto.NewPassword.Length < 8)
                return new ProfileUpdateResult(false, "New password must be at least 8 characters.");

            if (dto.NewPassword != dto.ConfirmPassword)
                return new ProfileUpdateResult(false, "Passwords do not match.");

            user.PasswordHash = PasswordHashHandler.HashPassword(dto.NewPassword);

            await _context.SaveChangesAsync();
            return new ProfileUpdateResult(true);
        }

        // =============== UPDATE PREFERENCES ===============
        public async Task<ProfileUpdateResult> UpdatePreferencesAsync(UserPreferenceDto prefs)
        {
            // EXAMPLE — pref’ler dilediğin gibi kaydedilecek

            return new ProfileUpdateResult(true);
        }
    }
}
