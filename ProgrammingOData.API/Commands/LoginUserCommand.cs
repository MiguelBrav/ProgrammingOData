using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class LoginUserCommand 
{
    public LoginUserDTO loginUser {  get; set; } = new LoginUserDTO();
}
