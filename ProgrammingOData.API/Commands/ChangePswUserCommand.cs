using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class ChangePswUserCommand : IRequest<IActionResult>
{
    public ChangePswDTO currentPsw {  get; set; } = new ChangePswDTO();
}
