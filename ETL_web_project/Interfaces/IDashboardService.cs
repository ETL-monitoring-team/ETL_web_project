using ETL_web_project.DTOs.Dashboard;

namespace ETL_web_project.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardSummaryDto> GetDashboardAsync();
        Task<DashboardSummaryDto> GetDashboardAsync(int rangeDays);
    }
}
