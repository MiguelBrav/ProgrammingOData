using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class CreatePrFrameWorkCommand : IRequest<IActionResult>
{
    public CreatePrFrameworkDTO createFramework {  get; set; } = new CreatePrFrameworkDTO();

}
