using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class CreatePrFrameWorkDescCommand : IRequest<IActionResult>
{
    public CreatePrFrameworkDescDTO CreatePrFrameWorkDesc {  get; set; } = new CreatePrFrameworkDescDTO();

}
