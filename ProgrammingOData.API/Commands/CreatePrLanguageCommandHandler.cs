using UseCaseCore.UseCases;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Commands;

public class CreatePrLanguageCommandHandler : UseCaseBase<CreatePrLanguageCommand, IActionResult>
{
    private readonly IPRLanguageRepository _prLanguageRepository;

    public CreatePrLanguageCommandHandler(IPRLanguageRepository prLanguageRepository)
    {
        _prLanguageRepository = prLanguageRepository;
    }

    public override async Task<IActionResult> Execute(CreatePrLanguageCommand request)
    {
        try
        {
            PrLanguage newLanguage = new PrLanguage(request.createLanguage.Name,
                 request.createLanguage.YearCreated,
                 request.createLanguage.Creator);

            int languageId = await _prLanguageRepository.Create(newLanguage);

            string newResourceUri = $"/prlanguages/by/{languageId}";
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
                Message = $"PrLanguage fail to be created"
            });
        }
    }
}
