using ETL_web_project.DTOs;
using ETL_web_project.DTOs.Etl.Schedule;

namespace ETL_web_project.Interfaces
{
    public interface IEtlScheduleOverviewService
    {
        Task<EtlScheduleOverviewDto> GetOverviewAsync();
    }
}
