using System.ComponentModel.DataAnnotations;

namespace ProgrammingOData.Models.DTOS;

public class DeleteByIdDTO
{
    [Required(ErrorMessage = "Id is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "The Id must be greater than 0.")]
    public int Id { get; set; }
}
