using MediatR;
using Microsoft.AspNetCore.OData.Results;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Queries;

public class ByIdPrLanguageQueryHandler : IRequestHandler<ByIdPrLanguageQuery, SingleResult<PrLanguage>>
{
    private readonly IPRLanguageRepository _prLanguageRepository;

    public ByIdPrLanguageQueryHandler(IPRLanguageRepository prLanguageRepository)
    {
        _prLanguageRepository = prLanguageRepository;
    }

    public async Task<SingleResult<PrLanguage>> Handle(ByIdPrLanguageQuery request, CancellationToken cancellationToken)
    {
        PrLanguage language = await _prLanguageRepository.GetById(request.Id);

        return SingleResult.Create(new List<PrLanguage> { language }.AsQueryable());
    }
}
