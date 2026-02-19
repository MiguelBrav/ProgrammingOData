using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class ChangePswUserCommand
{
    public ChangePswDTO currentPsw {  get; set; } = new ChangePswDTO();
}
