using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Commands;

public class CreatePrLanguageDescCommandHandler : UseCaseBase<CreatePrLanguageDescCommand, IActionResult>
{
    private readonly IPRLanguageRepository _prLanguageRepository;

    private readonly IPRLanguageDescriptionRepository _prLanguageDescRepository;


    public CreatePrLanguageDescCommandHandler(IPRLanguageRepository prLanguageRepository, IPRLanguageDescriptionRepository prLanguageDescRepository)
    {
        _prLanguageRepository = prLanguageRepository;
        _prLanguageDescRepository=prLanguageDescRepository;
    }

    public override async Task<IActionResult> Execute(CreatePrLanguageDescCommand request)
    {
        try
        {
            PrLanguage language = await _prLanguageRepository.GetById(request.createLanguageDesc.LanguageId) 
                ?? new PrLanguage();

            if (language.Id == 0)
                return new NoContentResult();


            int totalDescriptions = await _prLanguageDescRepository.CountByLanguageLocale
                (request.createLanguageDesc.LanguageId, request.createLanguageDesc.Locale);

            if (totalDescriptions > 0)
            {
                return new BadRequestObjectResult(new
                {
                    Message = "The language has an associated description. Update them.",
                });
            }

            PrLanguageDescription newDescription = new PrLanguageDescription(
                request.createLanguageDesc.LanguageId,
                 request.createLanguageDesc.Locale,
                 request.createLanguageDesc.Description);

            int languageDescId = await _prLanguageDescRepository.Create(newDescription);

            string newResourceUri = $"/prlanguagesdescriptions/by/{languageDescId}";
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
                Message = $"PrLanguageDescription fail to be created"
            });
        }
    }
}
