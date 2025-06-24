using System.ComponentModel.DataAnnotations;

namespace ProgrammingOData.Models.DTOS;

public class ConfirmPswDTO
{
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(255, ErrorMessage = "Password cannot exceed 255 characters.")]
    public string ConfirmPsw { get; set; } = string.Empty;

    [Required(ErrorMessage = "Token is required.")]
    public string Token { get; set; } = string.Empty;
}
