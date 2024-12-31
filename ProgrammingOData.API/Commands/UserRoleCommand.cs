using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class UserRoleCommand : IRequest<IActionResult>
{
    public UserRoleDTO userWithRol {  get; set; } = new UserRoleDTO();
}
