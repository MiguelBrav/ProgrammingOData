using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Commands;

public class DeletePrFrameworkCommandHandler : IRequestHandler<DeletePrFrameworkCommand, IActionResult>
{
    private readonly IPRFrameworkRepository _pRFrameworkRepository;

    private readonly IPRFrameworkDescriptionRepository _pRFrameworkDescriptionRepository;

    public DeletePrFrameworkCommandHandler(IPRFrameworkRepository pRFrameworkRepository, IPRFrameworkDescriptionRepository pRFrameworkDescriptionRepository)
    {
        _pRFrameworkRepository = pRFrameworkRepository;
        _pRFrameworkDescriptionRepository = pRFrameworkDescriptionRepository;
    }

    public async Task<IActionResult> Handle(DeletePrFrameworkCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int totalDescriptions = await _pRFrameworkDescriptionRepository.CountByLanguage(request.deleteLanguage.Id);

            if (totalDescriptions > 0)
            {
                return new BadRequestObjectResult(new
                {
                    Message = "The framework" +
                    " has associated descriptions. Delete them first.",
                });
            }

            await _pRFrameworkRepository.Delete(request.deleteLanguage.Id);

            return new OkObjectResult(new
            {
                Message = "PrFramework deleted successful"
            });
        }
        catch (Exception)
        {
            return new BadRequestObjectResult(new
            {
                Message = "PrFramework fail to be deleted"
            });
        }
    }
}
