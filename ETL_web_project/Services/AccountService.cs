using AutoMapper;
using ETL_web_project.Data.Context;
using ETL_web_project.Data.Entities;
using ETL_web_project.DTOs;
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

        // Login kontrolü
        public async Task<UserDto?> ValidateUserAsync(LoginDto loginDto)
        {
            var user = await _context.UserAccounts
                .FirstOrDefaultAsync(u =>
                    u.Username == loginDto.Username &&
                    u.PasswordHash == loginDto.Password &&   // şimdilik plain
                    u.IsActive);

            if (user == null)
                return null;

            // entity -> dto (mapper)
            var dto = _mapper.Map<UserDto>(user);

            // son login zamanını güncelle
            user.LastLoginAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.UserAccounts
                .AnyAsync(u => u.Username == username);
        }

        public async Task<UserDto> RegisterUserAsync(RegisterDto registerDto)
        {
            // RegisterDto -> UserAccount (entity) (mapper)
            var entity = _mapper.Map<UserAccount>(registerDto);

            entity.IsActive = true;
            entity.Role = Enums.UserRole.Analyst; // default rol
            entity.CreatedAt = DateTime.UtcNow;

            await _context.UserAccounts.AddAsync(entity);
            await _context.SaveChangesAsync();

            // entity -> UserDto
            return _mapper.Map<UserDto>(entity);
        }
    }
}
