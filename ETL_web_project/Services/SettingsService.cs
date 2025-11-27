using ETL_web_project.Data.Context;
using ETL_web_project.DTOs;
using ETL_web_project.Handlers;
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
                PasswordModel = new ChangePasswordDto { UserId = user.UserId }
            };
        }

        public async Task<ProfileUpdateResult> UpdateProfileAsync(ProfileSettingsDto dto)
        {
            var user = await _context.UserAccounts
                .FirstOrDefaultAsync(u => u.UserId == dto.UserId);

            if (user == null)
                return new ProfileUpdateResult(false, "User not found.");

            if (!PasswordHashHandler.VerifyPassword(dto.ConfirmPassword!, user.PasswordHash))
                return new ProfileUpdateResult(false, "Password is incorrect.");

            if (dto.Username.Length < 3)
                return new ProfileUpdateResult(false, "Username must be at least 3 characters.");

            if (await _context.UserAccounts.AnyAsync(u => u.UserId != dto.UserId && u.Username == dto.Username))
                return new ProfileUpdateResult(false, "This username is already in use.");

            if (await _context.UserAccounts.AnyAsync(u => u.UserId != dto.UserId && u.Email == dto.Email))
                return new ProfileUpdateResult(false, "This email is already in use.");

            user.Username = dto.Username;
            user.Email = dto.Email;

            await _context.SaveChangesAsync();
            return new ProfileUpdateResult(true);
        }

        public async Task<ProfileUpdateResult> ChangePasswordAsync(ChangePasswordDto dto)
        {
            var user = await _context.UserAccounts
                .FirstOrDefaultAsync(u => u.UserId == dto.UserId);

            if (user == null)
                return new ProfileUpdateResult(false, "User not found.");

            if (!PasswordHashHandler.VerifyPassword(dto.CurrentPassword, user.PasswordHash))
                return new ProfileUpdateResult(false, "Current password is incorrect.");

            if (dto.NewPassword.Length < 8)
                return new ProfileUpdateResult(false, "New password must be at least 8 characters.");

            if (dto.NewPassword != dto.ConfirmPassword)
                return new ProfileUpdateResult(false, "Passwords do not match.");

            user.PasswordHash = PasswordHashHandler.HashPassword(dto.NewPassword);

            await _context.SaveChangesAsync();
            return new ProfileUpdateResult(true);
        }
    }
}
