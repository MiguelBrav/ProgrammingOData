using System.ComponentModel.DataAnnotations;

namespace ProgrammingOData.Models.DTOS;

public class CreateUserDTO
{
    [Required(ErrorMessage = "Email is required.")]
    [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
    [EmailAddress(ErrorMessage = "Email invalid format.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(255, ErrorMessage = "Password cannot exceed 255 characters.")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "UserName is required.")]
    [StringLength(100, ErrorMessage = "UserName cannot exceed 100 characters.")]
    public string UserName { get; set; } = string.Empty;

    public DateTime? DateOfBirth { get; set; }
}
