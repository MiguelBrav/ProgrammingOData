using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class CreateLocaleCommand
{
    public CreateLocaleDTO createLocale {  get; set; } = new CreateLocaleDTO();

}
