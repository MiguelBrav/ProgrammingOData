using UseCaseCore.UseCases;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Commands;

public class CreatePrFrameWorkCommandHandler : UseCaseBase<CreatePrFrameWorkCommand, IActionResult>
{
    private readonly IPRFrameworkRepository _pRFrameworkRepository;
    private readonly IPRLanguageRepository _prLanguageRepository;


    public CreatePrFrameWorkCommandHandler(IPRFrameworkRepository pRFrameworkRepository, IPRLanguageRepository prLanguageRepository)
    {
        _pRFrameworkRepository = pRFrameworkRepository;
        _prLanguageRepository=prLanguageRepository;
    }

    public override async Task<IActionResult> Execute(CreatePrFrameWorkCommand request)
    {
        try
        {
            PrLanguage language = await _prLanguageRepository.GetById(request.createFramework.LanguageId)
             ?? new PrLanguage();

            if (language.Id == 0)
                return new BadRequestObjectResult(new { Message = "Language id is not valid" });

            PrFramework framework = new PrFramework(
                request.createFramework.Name,
                request.createFramework.LanguageId,
                request.createFramework.CreatedYear,
                request.createFramework.Creator,
                request.createFramework.Description
            );

            int frameworkId = await _pRFrameworkRepository.Create(framework);

            string newResourceUri = $"/prframeworks/by/{frameworkId}";
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
                Message = $"PrFramework fail to be created"
            });
        }
    }
}