using ETL_web_project.DTOs.Admin;

namespace ETL_web_project.ViewModels
{
    public class AdminDashboardViewModel
    {
        public List<AdminUserDto> Users { get; set; } = new();

        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int AdminCount { get; set; }
        public int AnalystCount { get; set; }
        public int DataEngineerCount { get; set; }
        public string? SearchTerm { get; set; }
    }
}
