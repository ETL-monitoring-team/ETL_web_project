using AutoMapper;
using ETL_web_project.Data.Context;
using ETL_web_project.DTOs.Admin;
using ETL_web_project.Enums;
using ETL_web_project.Interfaces;
using ETL_web_project.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ETL_web_project.Services
{
    public class AdminService : IAdminService
    {
        private readonly ProjectContext _context;
        private readonly IMapper _mapper;

        public AdminService(ProjectContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AdminDashboardViewModel> GetDashboardAsync(string? usernameFilter = null)
        {
            var query = _context.UserAccounts
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(usernameFilter))
            {
                var term = usernameFilter.Trim();
                query = query.Where(u => u.Username.Contains(term));
            }

            var users = await query
                .OrderByDescending(u => u.Role == UserRole.Admin)
                .ThenByDescending(u => u.Role == UserRole.DataEngineer)
                .ThenBy(u => u.CreatedAt)
                .ToListAsync();

            var userDtos = _mapper.Map<List<AdminUserDto>>(users);

            var vm = new AdminDashboardViewModel
            {
                Users = userDtos,
                TotalUsers = userDtos.Count,
                ActiveUsers = userDtos.Count(u => u.IsActive),
                AdminCount = userDtos.Count(u => u.Role == UserRole.Admin),
                AnalystCount = userDtos.Count(u => u.Role == UserRole.Analyst),
                DataEngineerCount = userDtos.Count(u => u.Role == UserRole.DataEngineer),
                SearchTerm = usernameFilter
            };

            return vm;
        }

        public async Task<bool> ChangeUserRoleAsync(int userId, UserRole newRole)
        {
            var user = await _context.UserAccounts.FindAsync(userId);
            if (user == null)
                return false;

            user.Role = newRole;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ToggleUserActiveAsync(int userId)
        {
            var user = await _context.UserAccounts.FindAsync(userId);
            if (user == null)
                return false;

            user.IsActive = !user.IsActive;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
