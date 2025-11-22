using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.API.Helpers;
using ProgrammingOData.API.Helpers.Enums;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Infrastructure.Repositories;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Commands;

public class CreatePrFrameWorkDescCommandHandler : IRequestHandler<CreatePrFrameWorkDescCommand, IActionResult>
{
    private readonly IPRFrameworkRepository _pRFrameworkRepository;
    private readonly IPRFrameworkDescriptionRepository _pRFrameworkDescriptionRepository;


    public CreatePrFrameWorkDescCommandHandler(IPRFrameworkRepository pRFrameworkRepository, IPRFrameworkDescriptionRepository pRFrameworkDescriptionRepository)
    {
        _pRFrameworkRepository = pRFrameworkRepository;
        _pRFrameworkDescriptionRepository=pRFrameworkDescriptionRepository;
    }

    public async Task<IActionResult> Handle(CreatePrFrameWorkDescCommand request, CancellationToken cancellationToken)
    {
        try
        {
            PrFramework framework = await _pRFrameworkRepository.GetById(request.CreatePrFrameWorkDesc.FrameworkId)
             ?? new PrFramework();

            if (framework.Id == 0)
                return new NoContentResult();


            int totalDescriptions = await _pRFrameworkDescriptionRepository.CountByLanguageLocale
                (request.CreatePrFrameWorkDesc.FrameworkId, request.CreatePrFrameWorkDesc.Locale);

            if (totalDescriptions > 0)
            {
                return new BadRequestObjectResult(new
                {
                    Message = "The frameworkId has an associated description. Update them.",
                });
            }

            PrFrameworkDescription newDescription = new PrFrameworkDescription(
                request.CreatePrFrameWorkDesc.FrameworkId,
                 request.CreatePrFrameWorkDesc.Locale,
                 request.CreatePrFrameWorkDesc.Description);

            int frameworkDescriptionId = await _pRFrameworkDescriptionRepository.Create(newDescription);

            string newResourceUri = $"/prframeworksdescriptions/by/{frameworkDescriptionId}";
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
                Message = $"PrFrameworkDescription fail to be created"
            });
        }
    }
}