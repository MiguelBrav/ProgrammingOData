using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Commands;

public class UpdatePrFrameWorkCommandHandler : UseCaseBase<UpdatePrFrameWorkCommand, IActionResult>
{
    private readonly IPRFrameworkRepository _pRFrameworkRepository;

    public UpdatePrFrameWorkCommandHandler(IPRFrameworkRepository pRFrameworkRepository)
    {
        _pRFrameworkRepository = pRFrameworkRepository;
    }

    public override async Task<IActionResult> Execute(UpdatePrFrameWorkCommand request)
    {
        try
        {
            PrFramework framework = await _pRFrameworkRepository.GetById(request.updatePrFramework.Id) ?? new PrFramework();

            if (framework.Id == 0)
                return new NoContentResult();

            framework.Name = !string.IsNullOrWhiteSpace(request.updatePrFramework.Name)
                ? request.updatePrFramework.Name
                : framework.Name;

            framework.LanguageId = request.updatePrFramework.LanguageId != 0
                ? request.updatePrFramework.LanguageId
                : framework.LanguageId;

            framework.CreatedYear = request.updatePrFramework.CreatedYear != 0
                ? request.updatePrFramework.CreatedYear
                : framework.CreatedYear;

            framework.Creator = !string.IsNullOrWhiteSpace(request.updatePrFramework.Creator)
                ? request.updatePrFramework.Creator
                : framework.Creator;

            framework.Description = !string.IsNullOrWhiteSpace(request.updatePrFramework.Description)
                ? request.updatePrFramework.Description
                : framework.Description;

            await _pRFrameworkRepository.Update(framework);

            return new OkObjectResult(new
            {
                Message = $"PrFramework updated successfully"
            });
        }
        catch (Exception)
        {
            return new BadRequestObjectResult(new
            {
                Message = $"PrFramework failed to be updated"
            });
        }
    }
}
