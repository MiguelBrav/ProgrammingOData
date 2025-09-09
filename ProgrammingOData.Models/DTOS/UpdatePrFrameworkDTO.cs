using System.ComponentModel.DataAnnotations;

namespace ProgrammingOData.Models.DTOS;

public class UpdatePrFrameworkDTO
{

    [Required(ErrorMessage = "Id is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "The Id must be greater than 0.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Language Id is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "The Language Id must be greater than 0.")]
    public int LanguageId { get; set; }

    [Required(ErrorMessage = "CreatedYear is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "The CreatedYear must be greater than 0.")]
    public int CreatedYear { get; set; }

    [Required(ErrorMessage = "Creator is required.")]
    [StringLength(100, ErrorMessage = "Creator cannot exceed 100 characters.")]
    public string Creator { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; } = string.Empty;
}
