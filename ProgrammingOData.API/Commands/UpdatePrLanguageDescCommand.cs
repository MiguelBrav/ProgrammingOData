using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class UpdatePrLanguageDescCommand : IRequest<IActionResult>
{
    public UpdatePrLanguageDescDTO updateLanguageDesc {  get; set; } = new UpdatePrLanguageDescDTO();

}
