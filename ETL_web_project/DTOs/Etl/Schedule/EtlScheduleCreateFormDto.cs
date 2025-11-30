using ETL_web_project.DTOs.Etl.Jobs;

namespace ETL_web_project.DTOs.Etl.Schedule
{
    public class EtlScheduleCreateFormDto
    {
        public List<EtlJobListItemDto> AvailableJobs { get; set; } = new();
        public EtlScheduleCreateDto CreateDto { get; set; } = new EtlScheduleCreateDto();
    }

}
