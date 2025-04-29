using System.ComponentModel.DataAnnotations;

namespace ProgrammingOData.Models.DTOS;

public class UpdateLocaleDTO
{
    [Required(ErrorMessage = "Id is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "The Id must be greater than 0.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Locale is required.")]
    [StringLength(10, ErrorMessage = "Locale cannot exceed 10 characters.")]
    public string Locale { get; set; } = string.Empty;

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
    public string Name { get; set; } = string.Empty;

}
