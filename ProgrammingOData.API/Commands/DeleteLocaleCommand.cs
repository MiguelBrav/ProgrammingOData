using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class DeleteLocaleCommand 
{
    public DeleteByIdDTO deleteLocale {  get; set; } = new DeleteByIdDTO();

}
