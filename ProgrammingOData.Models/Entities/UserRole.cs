namespace ProgrammingOData.Models.Entities;

public class RoleUser
{
    public Guid UserId { get; set; } 

    public string UserRole { get; set; } = string.Empty;

    public RoleUser(Guid userId, string userRole)
    {
        UserId = userId;
        UserRole = userRole;
    }
}
