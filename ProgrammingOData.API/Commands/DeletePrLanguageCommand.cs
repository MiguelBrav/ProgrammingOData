using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class DeletePrLanguageCommand 
{
    public DeleteByIdDTO deleteLanguage {  get; set; } = new DeleteByIdDTO();

}
