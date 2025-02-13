using MediatR;
using Microsoft.AspNetCore.OData.Results;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Queries;

public class ByIdPrLanguageDescriptionQueryHandler : IRequestHandler<ByIdPrLanguageDescriptionQuery, SingleResult<PrLanguageDescription>>
{
    private readonly IPRLanguageDescriptionRepository _prLanguageDescriptionRepository;

    public ByIdPrLanguageDescriptionQueryHandler(IPRLanguageDescriptionRepository prLanguageDescriptionRepository)
    {
        _prLanguageDescriptionRepository = prLanguageDescriptionRepository;
    }

    public async Task<SingleResult<PrLanguageDescription>> Handle(ByIdPrLanguageDescriptionQuery request, CancellationToken cancellationToken)
    {

        PrLanguageDescription languageDescription = await _prLanguageDescriptionRepository.GetById(request.Id);

        IQueryable<PrLanguageDescription> result = languageDescription is not null
          ? new List<PrLanguageDescription> { languageDescription }.AsQueryable()
          : Enumerable.Empty<PrLanguageDescription>().AsQueryable();

        return SingleResult.Create(result);
    }
}
