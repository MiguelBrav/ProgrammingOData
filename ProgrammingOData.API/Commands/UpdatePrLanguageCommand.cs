using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class UpdatePrLanguageCommand : IRequest<IActionResult>
{
    public UpdatePrLanguageDTO updateLanguage {  get; set; } = new UpdatePrLanguageDTO();

}
