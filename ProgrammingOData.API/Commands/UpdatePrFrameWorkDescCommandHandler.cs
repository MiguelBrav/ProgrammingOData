using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.API.Helpers;
using ProgrammingOData.API.Helpers.Enums;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Infrastructure.Repositories;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Commands;

public class UpdatePrFrameWorkDescCommandHandler : IRequestHandler<UpdatePrFrameWorkDescCommand, IActionResult>
{
    private readonly IPRFrameworkRepository _pRFrameworkRepository;
    private readonly IPRFrameworkDescriptionRepository _pRFrameworkDescriptionRepository;


    public UpdatePrFrameWorkDescCommandHandler(IPRFrameworkRepository pRFrameworkRepository, IPRFrameworkDescriptionRepository pRFrameworkDescriptionRepository)
    {
        _pRFrameworkRepository = pRFrameworkRepository;
        _pRFrameworkDescriptionRepository=pRFrameworkDescriptionRepository;
    }

    public async Task<IActionResult> Handle(UpdatePrFrameWorkDescCommand request, CancellationToken cancellationToken)
    {
        try
        {
            PrFrameworkDescription frameworkDesc = await _pRFrameworkDescriptionRepository.GetById(request.UpdatePrFrameworkDesc.Id)
             ?? new PrFrameworkDescription();

            if (frameworkDesc.Id == 0)
                return new NoContentResult();

            PrFramework framework = await _pRFrameworkRepository.GetById(request.UpdatePrFrameworkDesc.FrameworkId)
            ?? new PrFramework();

            if (framework.Id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    Message = $"Framework Id is not valid"
                });
            }  

            frameworkDesc.FrameworkId = request.UpdatePrFrameworkDesc.FrameworkId != 0
            ? request.UpdatePrFrameworkDesc.FrameworkId
            : frameworkDesc.FrameworkId;

            frameworkDesc.Locale = !string.IsNullOrWhiteSpace(request.UpdatePrFrameworkDesc.Locale)
                ? request.UpdatePrFrameworkDesc.Locale
                : frameworkDesc.Locale;

            frameworkDesc.Description = !string.IsNullOrWhiteSpace(request.UpdatePrFrameworkDesc.Description)
                ? request.UpdatePrFrameworkDesc.Description
                : frameworkDesc.Description;

           await _pRFrameworkDescriptionRepository.Update(frameworkDesc);

            return new OkObjectResult(new
            {
                Message = "PrFrameworkDescription updated successfully",
                UpdatedId = frameworkDesc.Id
            });
        }
        catch (Exception)
        {
            return new BadRequestObjectResult(new
            {
                Message = $"PrFrameworkDescription fail to be updated"
            });
        }
    }
}