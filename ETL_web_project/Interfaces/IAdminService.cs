using ETL_web_project.Enums;
using ETL_web_project.Models;

namespace ETL_web_project.Interfaces
{
    public interface IAdminService
    {
        Task<AdminDashboardViewModel> GetDashboardAsync(string? usernameFilter = null);

        Task<bool> ChangeUserRoleAsync(int userId, UserRole newRole);

        Task<bool> ToggleUserActiveAsync(int userId);
    }
}
