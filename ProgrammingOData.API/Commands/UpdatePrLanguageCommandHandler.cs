using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Commands;

public class UpdatePrLanguageCommandHandler : UseCaseBase<UpdatePrLanguageCommand, IActionResult>
{
    private readonly IPRLanguageRepository _prLanguageRepository;

    public UpdatePrLanguageCommandHandler(IPRLanguageRepository prLanguageRepository)
    {
        _prLanguageRepository = prLanguageRepository;
    }

    public override async Task<IActionResult> Execute(UpdatePrLanguageCommand request)
    {
        try
        {
            PrLanguage language = await _prLanguageRepository.GetById(request.updateLanguage.Id) ?? new PrLanguage();

            if (language.Id == 0)
                return new NoContentResult();

            language.Name = !string.IsNullOrWhiteSpace(request.updateLanguage.Name)
               ? request.updateLanguage.Name
               : language.Name;

            language.YearCreated = request.updateLanguage.YearCreated.HasValue
                ? request.updateLanguage.YearCreated.Value
                : language.YearCreated;

            language.Creator = !string.IsNullOrWhiteSpace(request.updateLanguage.Creator)
                ? request.updateLanguage.Creator
                : language.Creator;

            await _prLanguageRepository.Update(language);

            return new OkObjectResult(new
            {
                Message = "PrLanguage updated successful"
            });
        }
        catch (Exception)
        {
            return new BadRequestObjectResult(new
            {
                Message = $"PrLanguage fail to be updated"
            });
        }
    }
}
