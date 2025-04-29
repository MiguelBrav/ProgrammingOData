using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Hosting;
using ProgrammingOData.API.Helpers;
using ProgrammingOData.API.Helpers.Enums;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Infrastructure.Repositories;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Commands;

public class UpdateLocaleCommandHandler : IRequestHandler<UpdateLocaleCommand, IActionResult>
{
    private readonly ISupportedLocaleRepository _supportedLocaleRepository;

    public UpdateLocaleCommandHandler(ISupportedLocaleRepository supportedLocaleRepository)
    {
        _supportedLocaleRepository = supportedLocaleRepository;
    }

    public async Task<IActionResult> Handle(UpdateLocaleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            SupportedLocale locale = await _supportedLocaleRepository.GetById(request.updateLocale.Id) ?? new SupportedLocale();

            if (locale.Id == 0)
                return new NoContentResult();

            locale.Name = !string.IsNullOrWhiteSpace(request.updateLocale.Name)
               ? request.updateLocale.Name
               : locale.Name;

            locale.Locale = !string.IsNullOrWhiteSpace(request.updateLocale.Locale)
                ? request.updateLocale.Locale
                : locale.Locale;

            await _supportedLocaleRepository.Update(locale);

            return new OkObjectResult(new
            {
                Message = "Locale updated successful"
            });
        }
        catch (Exception)
        {
            return new BadRequestObjectResult(new
            {
                Message = $"Locale fail to be updated"
            });
        }
    }
}
