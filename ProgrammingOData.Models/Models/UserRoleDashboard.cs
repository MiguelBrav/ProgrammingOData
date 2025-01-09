namespace ProgrammingOData.Models.Models;

public class UserRoleDashboard
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public string UserRole { get; set; } = string.Empty;
}

