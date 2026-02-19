using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Commands;

public class CreateLocaleCommandHandler : UseCaseBase<CreateLocaleCommand, IActionResult>
{
    private readonly ISupportedLocaleRepository _supportedLocaleRepository;

    public CreateLocaleCommandHandler(ISupportedLocaleRepository supportedLocaleRepository)
    {
        _supportedLocaleRepository = supportedLocaleRepository;
    }

    public override async Task<IActionResult> Execute(CreateLocaleCommand request)
    {
        try
        {
            SupportedLocale newLocale = new SupportedLocale();

            SupportedLocale localeInactive = await _supportedLocaleRepository.GetByLocale(request.createLocale.Locale);

            if (localeInactive is not null && !localeInactive.IsActive)
            {
                newLocale = new SupportedLocale(localeInactive.Id, request.createLocale.Locale,
                     request.createLocale.Name, !localeInactive.IsActive);

                await _supportedLocaleRepository.Update(newLocale);

            }
            else if (localeInactive is not null && localeInactive.IsActive)
            {
                return new BadRequestObjectResult(new
                {
                    Message = $"Locale already exists"
                });
            }
            else
            {
                newLocale = new SupportedLocale(request.createLocale.Locale, request.createLocale.Name);

                newLocale.Id = await _supportedLocaleRepository.Create(newLocale);
            }

            string newResourceUri = $"/supportedlocales/by/{newLocale.Id}";
            return new CreatedResult(newResourceUri, new
            {
                CreatedAt = DateTime.UtcNow,
                Location = newResourceUri
            });
        }
        catch (Exception)
        {
            return new BadRequestObjectResult(new
            {
                Message = $"Locale fail to be created"
            });
        }
    }
}
