using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class UpdatePrFrameWorkCommand : IRequest<IActionResult>
{
    public UpdatePrFrameworkDTO updatePrFramework {  get; set; } = new UpdatePrFrameworkDTO();

}
