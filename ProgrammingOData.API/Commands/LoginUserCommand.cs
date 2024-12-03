using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class LoginUserCommand : IRequest<IActionResult>
{
    public LoginUserDTO loginUser {  get; set; } = new LoginUserDTO();
}
