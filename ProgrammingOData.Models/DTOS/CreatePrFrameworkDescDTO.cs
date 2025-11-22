using System.ComponentModel.DataAnnotations;

namespace ProgrammingOData.Models.DTOS;

public class CreatePrFrameworkDescDTO
{
    [Required(ErrorMessage = "FrameworkId is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "The FrameworkId must be greater than 0.")]
    public int FrameworkId { get; set; }

    [Required(ErrorMessage = "Locale is required.")]
    [StringLength(10, ErrorMessage = "Locale cannot exceed 100 characters.")]
    public string Locale { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; } = string.Empty;

}
