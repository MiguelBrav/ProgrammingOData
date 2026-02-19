using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class CreateUserCommand 
{
    public CreateUserDTO createUser {  get; set; } = new CreateUserDTO();
}
