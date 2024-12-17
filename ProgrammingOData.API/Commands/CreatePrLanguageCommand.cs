using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class CreatePrLanguageCommand : IRequest<IActionResult>
{
    public CreatePrLanguageDTO createLanguage {  get; set; } = new CreatePrLanguageDTO();

}
