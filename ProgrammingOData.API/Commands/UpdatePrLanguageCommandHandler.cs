using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Hosting;
using ProgrammingOData.API.Helpers;
using ProgrammingOData.API.Helpers.Enums;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Infrastructure.Repositories;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Commands;

public class UpdatePrLanguageCommandHandler : IRequestHandler<UpdatePrLanguageCommand, IActionResult>
{
    private readonly IPRLanguageRepository _prLanguageRepository;

    public UpdatePrLanguageCommandHandler(IPRLanguageRepository prLanguageRepository)
    {
        _prLanguageRepository = prLanguageRepository;
    }

    public async Task<IActionResult> Handle(UpdatePrLanguageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            PrLanguage language = await _prLanguageRepository.GetById(request.updateLanguage.Id) ?? new PrLanguage();

            if (language.Id == 0)
                return new NoContentResult();

            language.Name = !string.IsNullOrWhiteSpace(request.updateLanguage.Name)
               ? request.updateLanguage.Name
               : language.Name;

            language.YearCreated = request.updateLanguage.YearCreated.HasValue
                ? request.updateLanguage.YearCreated.Value
                : language.YearCreated;

            language.Creator = !string.IsNullOrWhiteSpace(request.updateLanguage.Creator)
                ? request.updateLanguage.Creator
                : language.Creator;

            await _prLanguageRepository.Update(language);

            return new OkObjectResult(new
            {
                Message = "PrLanguage updated successful"
            });
        }
        catch (Exception)
        {
            return new BadRequestObjectResult(new
            {
                Message = $"PrLanguage fail to be updated"
            });
        }
    }
}
