using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class ConfirmPswUserCommand 
{
    public ConfirmPswDTO confirmPsw {  get; set; } = new ConfirmPswDTO();
}
