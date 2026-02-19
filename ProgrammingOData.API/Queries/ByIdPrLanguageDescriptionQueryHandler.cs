using Microsoft.AspNetCore.OData.Results;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Queries;

public class ByIdPrLanguageDescriptionQueryHandler : UseCaseBase<ByIdPrLanguageDescriptionQuery, SingleResult<PrLanguageDescription>>
{
    private readonly IPRLanguageDescriptionRepository _prLanguageDescriptionRepository;

    public ByIdPrLanguageDescriptionQueryHandler(IPRLanguageDescriptionRepository prLanguageDescriptionRepository)
    {
        _prLanguageDescriptionRepository = prLanguageDescriptionRepository;
    }

    public override async Task<SingleResult<PrLanguageDescription>> Execute(ByIdPrLanguageDescriptionQuery request)
    {

        PrLanguageDescription languageDescription = await _prLanguageDescriptionRepository.GetById(request.Id);

        IQueryable<PrLanguageDescription> result = languageDescription is not null
          ? new List<PrLanguageDescription> { languageDescription }.AsQueryable()
          : Enumerable.Empty<PrLanguageDescription>().AsQueryable();

        return SingleResult.Create(result);
    }
}
