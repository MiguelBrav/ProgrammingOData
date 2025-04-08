using System.ComponentModel.DataAnnotations;

namespace ProgrammingOData.Models.DTOS;

public class UpdatePrLanguageDescDTO
{
    [Required(ErrorMessage = "Id is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "The Id must be greater than 0.")]
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "The Id must be greater than 0.")]
    public int? LanguageId { get; set; }

    [StringLength(10, ErrorMessage = "Locale cannot exceed 100 characters.")]
    public string? Locale { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;

}
