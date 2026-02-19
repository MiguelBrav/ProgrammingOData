using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Domain.Interfaces;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Commands;

public class DeleteLocaleCommandHandler : UseCaseBase<DeleteLocaleCommand, IActionResult>
{
    private readonly ISupportedLocaleRepository _supportedLocaleRepository;

    public DeleteLocaleCommandHandler(ISupportedLocaleRepository supportedLocaleRepository)
    {
        _supportedLocaleRepository = supportedLocaleRepository;
    }

    public override async Task<IActionResult> Execute(DeleteLocaleCommand request)
    {
        try
        {
            await _supportedLocaleRepository.SetLocaleActiveStatus(request.deleteLocale.Id, false);

            return new OkObjectResult(new
            {
                Message = "Locale deleted successful"
            });
        }
        catch (Exception)
        {
            return new BadRequestObjectResult(new
            {
                Message = "Locale fail to be deleted"
            });
        }
    }
}
