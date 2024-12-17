using System.ComponentModel.DataAnnotations;

namespace ProgrammingOData.Models.DTOS;

public class LoginUserDTO
{
    [Required(ErrorMessage = "Email is required.")]
    [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
    [EmailAddress(ErrorMessage = "Email invalid format.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(255, ErrorMessage = "Password cannot exceed 255 characters.")]
    public string Password { get; set; } = string.Empty;

}
