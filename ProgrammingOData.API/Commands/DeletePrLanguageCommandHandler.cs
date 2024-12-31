using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Commands;

public class DeletePrLanguageCommandHandler : IRequestHandler<DeletePrLanguageCommand, IActionResult>
{
    private readonly IPRLanguageRepository _prLanguageRepository;

    public DeletePrLanguageCommandHandler(IPRLanguageRepository prLanguageRepository)
    {
        _prLanguageRepository = prLanguageRepository;
    }

    public async Task<IActionResult> Handle(DeletePrLanguageCommand request, CancellationToken cancellationToken)
    {
        try
        {
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
