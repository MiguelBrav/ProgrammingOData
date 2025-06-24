using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class ConfirmPswUserCommand : IRequest<IActionResult>
{
    public ConfirmPswDTO confirmPsw {  get; set; } = new ConfirmPswDTO();
}
