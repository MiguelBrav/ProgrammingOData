using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class UpdatePrFrameWorkDescCommand : IRequest<IActionResult>
{
    public UpdatePrFrameworkDescDTO UpdatePrFrameworkDesc {  get; set; } = new UpdatePrFrameworkDescDTO();

}
