namespace ETL_web_project.DTOs
{
    public class ProfileUpdateResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }

        public ProfileUpdateResult(bool success, string? errorMessage = null)
        {
            Success = success;
            ErrorMessage = errorMessage;
        }
    }
}
