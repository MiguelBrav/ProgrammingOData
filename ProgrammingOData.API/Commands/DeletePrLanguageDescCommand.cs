using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class DeletePrLanguageDescCommand : IRequest<IActionResult>
{
    public DeleteByIdDTO deleteLanguage {  get; set; } = new DeleteByIdDTO();

}
