using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class UserRoleCommand 
{
    public UserRoleDTO userWithRol {  get; set; } = new UserRoleDTO();
}
