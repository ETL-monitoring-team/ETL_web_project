namespace ETL_web_project.DTOs.Etl.Staging
{
    public class StagingErrorLogDto
    {
        public DateTime LogTime { get; set; }
        public Enums.LogLevel Level { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
