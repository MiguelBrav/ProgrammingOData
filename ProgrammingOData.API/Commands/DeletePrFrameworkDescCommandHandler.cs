using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Domain.Interfaces;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Commands;

public class DeletePrFrameworkDescCommandHandler : UseCaseBase<DeletePrFrameworkDescCommand, IActionResult>
{

    private readonly IPRFrameworkDescriptionRepository _prFrameworkDescriptionRepository;

    public DeletePrFrameworkDescCommandHandler(IPRFrameworkDescriptionRepository prFrameworkDescriptionRepository)
    {
        _prFrameworkDescriptionRepository = prFrameworkDescriptionRepository;
    }

    public override async Task<IActionResult> Execute(DeletePrFrameworkDescCommand request)
    {
        try
        {

            await _prFrameworkDescriptionRepository.Delete(request.deleteFramework.Id);

            return new OkObjectResult(new
            {
                Message = "PrFramework description deleted successful"
            });
        }
        catch (Exception)
        {
            return new BadRequestObjectResult(new
            {
                Message = "PrFramework description fail to be deleted"
            });
        }
    }
}
