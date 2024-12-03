namespace ProgrammingOData.Models.DTOS;

public class CreateUserDTO
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public DateTime? DateOfBirth { get; set; }
}
