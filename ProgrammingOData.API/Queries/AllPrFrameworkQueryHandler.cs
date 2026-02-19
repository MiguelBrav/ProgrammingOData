using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Queries;

public class AllPrFrameworkQueryHandler : UseCaseBase<AllPrFrameworkQuery, IQueryable<PrFramework>>
{
    private readonly IPRFrameworkRepository _prFrameworkRepository;

    private readonly IConfiguration _configuration;

    private string _defaultLocale;

    public AllPrFrameworkQueryHandler(IPRFrameworkRepository prFrameworkRepository, IConfiguration configuration)
    {
        _prFrameworkRepository = prFrameworkRepository;
        _configuration = configuration;
        _defaultLocale = _configuration.GetValue<string>("DefaultLocale") ?? string.Empty;
    }

    public override async Task<IQueryable<PrFramework>> Execute(AllPrFrameworkQuery request)
    {
        if (string.IsNullOrEmpty(request.Locale))
        {
            request.Locale = _defaultLocale;    
        }

        List<PrFramework> frameworks = await _prFrameworkRepository.GetAllByLocale(request.Locale) ?? new List<PrFramework>();

        return frameworks.AsQueryable();
    }
}
