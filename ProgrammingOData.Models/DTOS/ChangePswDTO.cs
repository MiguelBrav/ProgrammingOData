using System.ComponentModel.DataAnnotations;

namespace ProgrammingOData.Models.DTOS;

public class ChangePswDTO
{
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(255, ErrorMessage = "Password cannot exceed 255 characters.")]
    public string ConfirmPsw { get; set; } = string.Empty;
}
