using System.ComponentModel.DataAnnotations;

namespace ProgrammingOData.Models.DTOS;

public class UserRoleDTO
{
    [Required(ErrorMessage = "Email is required.")]
    [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(255, ErrorMessage = "Password cannot exceed 255 characters.")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "UserRole is required.")]
    [StringLength(10, ErrorMessage = "UserRole cannot exceed 10 characters.")]
    public string UserRole {  get; set; } = string.Empty;

}
