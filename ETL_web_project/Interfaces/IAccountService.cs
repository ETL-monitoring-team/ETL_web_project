using ETL_web_project.DTOs;

namespace ETL_web_project.Interfaces
{
    public interface IAccountService
    {
        Task<UserDto?> ValidateUserAsync(LoginDto loginDto);
        Task<bool> UsernameExistsAsync(string username);
        Task<bool> EmailExistsAsync(string email);
        Task<UserDto> RegisterUserAsync(RegisterDto registerDto);
        Task<bool> ResetPasswordAsync(string token, string newPassword);
        Task<string> GeneratePasswordResetTokenAsync(string email);


    }
}
