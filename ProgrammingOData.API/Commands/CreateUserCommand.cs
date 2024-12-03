using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class CreateUserCommand : IRequest<IActionResult>
{
    public CreateUserDTO createUser {  get; set; } = new CreateUserDTO();
}
