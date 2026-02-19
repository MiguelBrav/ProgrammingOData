using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Domain.Interfaces;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Commands;

public class DeletePrLanguageDescCommandHandler : UseCaseBase<DeletePrLanguageDescCommand, IActionResult>
{

    private readonly IPRLanguageDescriptionRepository _prLanguageDescriptionRepository;

    public DeletePrLanguageDescCommandHandler(IPRLanguageDescriptionRepository prLanguageDescriptionRepository)
    {
        _prLanguageDescriptionRepository = prLanguageDescriptionRepository;
    }

    public override async Task<IActionResult> Execute(DeletePrLanguageDescCommand request)
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
