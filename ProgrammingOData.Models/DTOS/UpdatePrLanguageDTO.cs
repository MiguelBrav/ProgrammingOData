using System.ComponentModel.DataAnnotations;

namespace ProgrammingOData.Models.DTOS;

public class UpdatePrLanguageDTO
{
    [Required(ErrorMessage = "Id is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "The Id must be greater than 0.")]
    public int Id { get; set; }

    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string? Name { get; set; } 

    [Range(1, int.MaxValue, ErrorMessage = "The YearCreated must be greater than 0.")]
    public int? YearCreated { get; set; }

    [StringLength(100, ErrorMessage = "Creator cannot exceed 100 characters.")]
    public string? Creator { get; set; } 
}
