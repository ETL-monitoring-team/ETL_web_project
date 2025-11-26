using AutoMapper;
using ETL_web_project.Data.Context;
using ETL_web_project.Data.Entities;
using ETL_web_project.DTOs;
using ETL_web_project.Handlers;
using ETL_web_project.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ETL_web_project.Services
{
    public class AccountService : IAccountService
    {
        private readonly ProjectContext _context;
        private readonly IMapper _mapper;

        public AccountService(ProjectContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto?> ValidateUserAsync(LoginDto loginDto)
        {
            var user = await _context.UserAccounts
               .FirstOrDefaultAsync(u =>
                   u.Username == loginDto.Username &&
                   u.IsActive);

            if (user == null)
                return null;

            if (!PasswordHashHandler.VerifyPassword(loginDto.Password, user.PasswordHash))
                return null;

            user.LastLoginAt = DateTime.Now;

            _context.UserAccounts.Update(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.UserAccounts.AnyAsync(u => u.Username == username);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.UserAccounts.AnyAsync(u => u.Email == email);
        }

        public async Task<UserDto> RegisterUserAsync(RegisterDto registerDto)
        {
            var entity = _mapper.Map<UserAccount>(registerDto);

            entity.IsActive = true;
            entity.Role = Enums.UserRole.Analyst;
            entity.CreatedAt = DateTime.Now;

            await _context.UserAccounts.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(entity);
        }
    }
}
