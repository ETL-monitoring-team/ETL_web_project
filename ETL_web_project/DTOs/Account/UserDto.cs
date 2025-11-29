using ETL_web_project.Enums;

namespace ETL_web_project.DTOs.Account
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public UserRole Role { get; set; }

        public bool IsActive { get; set; }
    }
}
