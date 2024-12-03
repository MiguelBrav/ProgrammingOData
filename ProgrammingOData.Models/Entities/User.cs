using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.Models.Entities;

public class User
{
    public Guid UserId { get; set; } 
    public string Email { get; set; } = string.Empty; 
    public string EmailNormalized { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; } 
    public string Password { get; set; } = string.Empty;
    public User() { }

    public User(string email, string password, string userName, DateTime? dateOfBirth = null)
    {
        UserId = Guid.NewGuid();
        Email = email;
        EmailNormalized = email.ToUpperInvariant(); 
        UserName = userName;
        DateOfBirth = dateOfBirth.HasValue ? dateOfBirth : null;
        Password = password; 
    }
}


 
