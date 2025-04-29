using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class CreateLocaleCommand : IRequest<IActionResult>
{
    public CreateLocaleDTO createLocale {  get; set; } = new CreateLocaleDTO();

}
