using ETL_web_project.DTOs;

namespace ETL_web_project.Interfaces
{
    public interface IAccountService
    {
        Task<UserDto?> ValidateUserAsync(LoginDto loginDto);
        Task<bool> UsernameExistsAsync(string username);
        Task<UserDto> RegisterUserAsync(RegisterDto registerDto);
    }
}
