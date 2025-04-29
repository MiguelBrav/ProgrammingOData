using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class DeleteLocaleCommand : IRequest<IActionResult>
{
    public DeleteByIdDTO deleteLocale {  get; set; } = new DeleteByIdDTO();

}
