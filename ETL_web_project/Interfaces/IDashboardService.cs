using ETL_web_project.DTOs;

namespace ETL_web_project.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardSummaryDto> GetDashboardAsync();
    }
}
