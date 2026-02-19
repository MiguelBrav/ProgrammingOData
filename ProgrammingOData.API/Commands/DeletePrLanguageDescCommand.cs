using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class DeletePrLanguageDescCommand
{
    public DeleteByIdDTO deleteLanguage {  get; set; } = new DeleteByIdDTO();

}
