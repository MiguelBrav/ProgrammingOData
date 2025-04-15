using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Commands;

public class DeletePrLanguageDescCommandHandler : IRequestHandler<DeletePrLanguageDescCommand, IActionResult>
{

    private readonly IPRLanguageDescriptionRepository _prLanguageDescriptionRepository;

    public DeletePrLanguageDescCommandHandler(IPRLanguageDescriptionRepository prLanguageDescriptionRepository)
    {
        _prLanguageDescriptionRepository = prLanguageDescriptionRepository;
    }

    public async Task<IActionResult> Handle(DeletePrLanguageDescCommand request, CancellationToken cancellationToken)
    {
        try
        {

            await _prLanguageDescriptionRepository.Delete(request.deleteLanguage.Id);

            return new OkObjectResult(new
            {
                Message = "PrLanguage description deleted successful"
            });
        }
        catch (Exception)
        {
            return new BadRequestObjectResult(new
            {
                Message = "PrLanguage description fail to be deleted"
            });
        }
    }
}
