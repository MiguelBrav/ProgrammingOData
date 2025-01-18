using MediatR;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Queries;

public class AllShortyQueryHandler : IRequestHandler<AllPrLanguageQuery, IQueryable<PrLanguage>>
{
    private readonly IPRLanguageRepository _prLanguageRepository;

    private readonly IConfiguration _configuration;

    private string _defaultLocale;

    public AllShortyQueryHandler(IPRLanguageRepository prLanguageRepository, IConfiguration configuration)
    {
        _prLanguageRepository = prLanguageRepository;
        _configuration = configuration;
        _defaultLocale = _configuration.GetValue<string>("DefaultLocale") ?? string.Empty;
    }

    public async Task<IQueryable<PrLanguage>> Handle(AllPrLanguageQuery request, CancellationToken cancellationToken)
    {

        if (string.IsNullOrEmpty(request.Locale))
        {
            request.Locale = _defaultLocale;    
        }

        List<PrLanguage> languages = await _prLanguageRepository.GetAllByLocale(request.Locale) ?? new List<PrLanguage>();

        return languages.AsQueryable();
    }
}
