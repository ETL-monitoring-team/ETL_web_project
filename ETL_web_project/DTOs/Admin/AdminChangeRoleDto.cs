namespace ETL_web_project.DTOs.Admin
{
    public class AdminChangeRoleDto
    {
        public int UserId { get; set; }
        public string Role { get; set; } = null!;
        public string AdminPassword { get; set; } = null!;
    }
}
