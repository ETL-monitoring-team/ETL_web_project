namespace ETL_web_project.DTOs
{
    public class UserPreferenceDto
    {
        public bool UseDarkTheme { get; set; } = true;
        public bool NotifyOnJobFailed { get; set; } = true;
        public bool NotifyOnJobSuccess { get; set; } = true;
    }
}
