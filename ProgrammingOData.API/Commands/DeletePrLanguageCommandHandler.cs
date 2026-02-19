using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Domain.Interfaces;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Commands;

public class DeletePrLanguageCommandHandler : UseCaseBase<DeletePrLanguageCommand, IActionResult>
{
    private readonly IPRLanguageRepository _prLanguageRepository;

    private readonly IPRLanguageDescriptionRepository _prLanguageDescriptionRepository;

    public DeletePrLanguageCommandHandler(IPRLanguageRepository prLanguageRepository, IPRLanguageDescriptionRepository prLanguageDescriptionRepository)
    {
        _prLanguageRepository = prLanguageRepository;
        _prLanguageDescriptionRepository = prLanguageDescriptionRepository;
    }

    public override async Task<IActionResult> Execute(DeletePrLanguageCommand request)
    {
        try
        {
            int totalDescriptions = await _prLanguageDescriptionRepository.CountByLanguage(request.deleteLanguage.Id);

            if (totalDescriptions > 0)
            {
                return new BadRequestObjectResult(new
                {
                    Message = "The language has associated descriptions. Delete them first.",
                });
            }

            await _prLanguageRepository.Delete(request.deleteLanguage.Id);

            return new OkObjectResult(new
            {
                Message = "PrLanguage deleted successful"
            });
        }
        catch (Exception)
        {
            return new BadRequestObjectResult(new
            {
                Message = "PrLanguage fail to be deleted"
            });
        }
    }
}
