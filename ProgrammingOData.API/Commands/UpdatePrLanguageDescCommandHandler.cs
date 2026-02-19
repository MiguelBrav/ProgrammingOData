using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Commands;

public class UpdatePrLanguageDescCommandHandler : UseCaseBase<UpdatePrLanguageDescCommand, IActionResult>
{
    private readonly IPRLanguageDescriptionRepository _prLanguageDescRepository;

    public UpdatePrLanguageDescCommandHandler(IPRLanguageDescriptionRepository prLanguageDescRepository)
    {
        _prLanguageDescRepository = prLanguageDescRepository;
    }

    public override async Task<IActionResult> Execute(UpdatePrLanguageDescCommand request)
    {
        try
        {
            PrLanguageDescription languageDesc = await _prLanguageDescRepository.GetById(request.updateLanguageDesc.Id)
                ?? new PrLanguageDescription();

            if (languageDesc.Id == 0)
                return new NoContentResult();

            languageDesc.Locale = !string.IsNullOrWhiteSpace(request.updateLanguageDesc.Locale)
               ? request.updateLanguageDesc.Locale
               : languageDesc.Locale;

            languageDesc.LanguageId = request.updateLanguageDesc.LanguageId.HasValue
                ? request.updateLanguageDesc.LanguageId.Value
                : languageDesc.LanguageId;

            languageDesc.Description = !string.IsNullOrWhiteSpace(request.updateLanguageDesc.Description)
                ? request.updateLanguageDesc.Description
                : languageDesc.Description;

            await _prLanguageDescRepository.Update(languageDesc);

            return new OkObjectResult(new
            {
                Message = "PrLanguage Description updated successful"
            });
        }
        catch (Exception)
        {
            return new BadRequestObjectResult(new
            {
                Message = $"PrLanguage Description fail to be updated"
            });
        }
    }
}
