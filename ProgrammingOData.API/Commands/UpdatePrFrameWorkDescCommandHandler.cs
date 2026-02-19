using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Commands;

public class UpdatePrFrameWorkDescCommandHandler : UseCaseBase<UpdatePrFrameWorkDescCommand, IActionResult>
{
    private readonly IPRFrameworkRepository _pRFrameworkRepository;
    private readonly IPRFrameworkDescriptionRepository _pRFrameworkDescriptionRepository;


    public UpdatePrFrameWorkDescCommandHandler(IPRFrameworkRepository pRFrameworkRepository, IPRFrameworkDescriptionRepository pRFrameworkDescriptionRepository)
    {
        _pRFrameworkRepository = pRFrameworkRepository;
        _pRFrameworkDescriptionRepository=pRFrameworkDescriptionRepository;
    }

    public override async Task<IActionResult> Execute(UpdatePrFrameWorkDescCommand request)
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