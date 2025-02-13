using MediatR;
using Microsoft.AspNetCore.OData.Results;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;
using System.Configuration;

namespace ProgrammingOData.API.Queries;

public class ByIdPrLanguageQueryHandler : IRequestHandler<ByIdPrLanguageQuery, SingleResult<PrLanguage>>
{
    private readonly IPRLanguageRepository _prLanguageRepository;

    private readonly IConfiguration _configuration;

    private string _defaultLocale;

    public ByIdPrLanguageQueryHandler(IPRLanguageRepository prLanguageRepository,IConfiguration configuration)
    {
        _prLanguageRepository = prLanguageRepository;
        _configuration = configuration;
        _defaultLocale = _configuration.GetValue<string>("DefaultLocale") ?? string.Empty;
    }

    public async Task<SingleResult<PrLanguage>> Handle(ByIdPrLanguageQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Locale))
        {
            request.Locale = _defaultLocale;
        }

        PrLanguage language = await _prLanguageRepository.GetByIdAndLocale(request.Id, request.Locale);

        IQueryable<PrLanguage> result = language is not null
             ? new List<PrLanguage> { language }.AsQueryable()
             : Enumerable.Empty<PrLanguage>().AsQueryable();

        return SingleResult.Create(result);
    }
}
