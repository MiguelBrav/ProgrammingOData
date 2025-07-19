using MediatR;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Queries;

public class AllPrFrameworkQueryHandler : IRequestHandler<AllPrFrameworkQuery, IQueryable<PrFramework>>
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

    public async Task<IQueryable<PrFramework>> Handle(AllPrFrameworkQuery request, CancellationToken cancellationToken)
    {

        if (string.IsNullOrEmpty(request.Locale))
        {
            request.Locale = _defaultLocale;    
        }

        List<PrFramework> frameworks = await _prFrameworkRepository.GetAllByLocale(request.Locale) ?? new List<PrFramework>();

        return frameworks.AsQueryable();
    }
}
