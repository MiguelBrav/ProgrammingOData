namespace ProgrammingOData.Models.Entities;

public class UserWithRol
{
    public Guid UserId { get; set; } 
    public string Email { get; set; } = string.Empty; 
    public string Password { get; set; } = string.Empty;
    public string UserRole { get; set; } = string.Empty;
}



