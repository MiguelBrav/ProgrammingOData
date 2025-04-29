using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Commands;

public class DeleteLocaleCommandHandler : IRequestHandler<DeleteLocaleCommand, IActionResult>
{
    private readonly ISupportedLocaleRepository _supportedLocaleRepository;

    public DeleteLocaleCommandHandler(ISupportedLocaleRepository supportedLocaleRepository)
    {
        _supportedLocaleRepository = supportedLocaleRepository;
    }

    public async Task<IActionResult> Handle(DeleteLocaleCommand request, CancellationToken cancellationToken)
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
