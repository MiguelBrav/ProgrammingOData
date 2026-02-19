using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Commands;

public class UpdateLocaleCommandHandler : UseCaseBase<UpdateLocaleCommand, IActionResult>
{
    private readonly ISupportedLocaleRepository _supportedLocaleRepository;

    public UpdateLocaleCommandHandler(ISupportedLocaleRepository supportedLocaleRepository)
    {
        _supportedLocaleRepository = supportedLocaleRepository;
    }

    public override async Task<IActionResult> Execute(UpdateLocaleCommand request)
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
