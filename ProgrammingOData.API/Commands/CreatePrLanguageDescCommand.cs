using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class CreatePrLanguageDescCommand : IRequest<IActionResult>
{
    public CreatePrLanguageDescDTO createLanguageDesc {  get; set; } = new CreatePrLanguageDescDTO();

}
