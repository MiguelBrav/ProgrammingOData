using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Models.DTOS;

namespace ProgrammingOData.API.Commands; 

public class UpdateLocaleCommand : IRequest<IActionResult>
{
    public UpdateLocaleDTO updateLocale {  get; set; } = new UpdateLocaleDTO();

}
