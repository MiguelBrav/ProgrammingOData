using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class UpdateLocaleCommand
{
    public UpdateLocaleDTO updateLocale {  get; set; } = new UpdateLocaleDTO();

}
