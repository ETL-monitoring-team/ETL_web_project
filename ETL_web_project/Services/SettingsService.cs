using ETL_web_project.Data.Context;
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

        // 1) Sayfa yüklenirken tüm modelleri doldur
        public async Task<SettingsViewModel> GetSettingsForUserAsync(int userId)
        {
            var user = await _context.UserAccounts
                .AsNoTracking()
                .FirstAsync(u => u.UserId == userId);

            var vm = new SettingsViewModel
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
                PasswordModel = new ChangePasswordDto
                {
                    UserId = user.UserId
                },
                Preferences = new UserPreferenceDto()
            };

            return vm;
        }

        // 2) Profil (username + email) güncelle
        public async Task<bool> UpdateProfileAsync(ProfileSettingsDto dto)
        {
            var u = await _context.UserAccounts.FindAsync(dto.UserId);
            if (u == null) return false;

            // DTO zaten DataAnnotation ile valide edildi
            u.Username = dto.Username;
            u.Email = dto.Email;

            await _context.SaveChangesAsync();
            return true;
        }

        // 3) Şifre değiştir
        public async Task<bool> ChangePasswordAsync(ChangePasswordDto dto)
        {
            var u = await _context.UserAccounts.FindAsync(dto.UserId);
            if (u == null) return false;

            var ok = PasswordHashHandler.VerifyPassword(dto.CurrentPassword, u.PasswordHash);
            if (!ok) return false;

            u.PasswordHash = PasswordHashHandler.HashPassword(dto.NewPassword);

            await _context.SaveChangesAsync();
            return true;
        }

        // 4) Kullanıcı tercihleri (şimdilik DB yok, ileride eklenecek)
        public Task UpdatePreferencesAsync(int userId, UserPreferenceDto dto)
        {
            // İleride ayrı tabloya kaydedebilirsin.
            return Task.CompletedTask;
        }
    }
}
