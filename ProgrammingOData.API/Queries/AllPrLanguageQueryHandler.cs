
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Queries;

public class AllPrLanguageQueryHandler : UseCaseBase<AllPrLanguageQuery, IQueryable<PrLanguage>>
{
    private readonly IPRLanguageRepository _prLanguageRepository;

    private readonly IConfiguration _configuration;

    private string _defaultLocale;

    public AllPrLanguageQueryHandler(IPRLanguageRepository prLanguageRepository, IConfiguration configuration)
    {
        _prLanguageRepository = prLanguageRepository;
        _configuration = configuration;
        _defaultLocale = _configuration.GetValue<string>("DefaultLocale") ?? string.Empty;
    }

    public override async Task<IQueryable<PrLanguage>> Execute(AllPrLanguageQuery request)
    {

        if (string.IsNullOrEmpty(request.Locale))
        {
            request.Locale = _defaultLocale;    
        }

        List<PrLanguage> languages = await _prLanguageRepository.GetAllByLocale(request.Locale) ?? new List<PrLanguage>();

        return languages.AsQueryable();
    }
}
