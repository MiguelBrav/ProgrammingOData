using System.ComponentModel.DataAnnotations;

namespace ProgrammingOData.Models.DTOS;

public class CreatePrLanguageDTO
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "YearCreated is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "The YearCreated must be greater than 0.")]
    public int YearCreated { get; set; }

    [Required(ErrorMessage = "Creator is required.")]
    [StringLength(100, ErrorMessage = "Creator cannot exceed 100 characters.")]
    public string Creator { get; set; } = string.Empty;
}
