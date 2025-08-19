using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.API.Helpers;
using ProgrammingOData.API.Helpers.Enums;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Infrastructure.Repositories;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Commands;

public class CreatePrFrameWorkCommandHandler : IRequestHandler<CreatePrFrameWorkCommand, IActionResult>
{
    private readonly IPRFrameworkRepository _pRFrameworkRepository;
    private readonly IPRLanguageRepository _prLanguageRepository;


    public CreatePrFrameWorkCommandHandler(IPRFrameworkRepository pRFrameworkRepository, IPRLanguageRepository prLanguageRepository)
    {
        _pRFrameworkRepository = pRFrameworkRepository;
        _prLanguageRepository=prLanguageRepository;
    }

    public async Task<IActionResult> Handle(CreatePrFrameWorkCommand request, CancellationToken cancellationToken)
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