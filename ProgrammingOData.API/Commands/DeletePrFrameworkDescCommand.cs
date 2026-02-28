using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class DeletePrFrameworkDescCommand
{
    public DeleteByIdDTO deleteFramework {  get; set; } = new DeleteByIdDTO();

}
