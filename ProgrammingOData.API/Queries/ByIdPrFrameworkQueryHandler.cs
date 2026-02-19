using Microsoft.AspNetCore.OData.Results;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Queries;

public class ByIdPrFrameworkQueryHandler : UseCaseBase<ByIdPrFrameworkQuery, SingleResult<PrFramework>>
{
    private readonly IPRFrameworkRepository _pRFrameworkRepository;

    private readonly IConfiguration _configuration;

    private string _defaultLocale;

    public ByIdPrFrameworkQueryHandler(IPRFrameworkRepository pRFrameworkRepository, IConfiguration configuration)
    {
        _pRFrameworkRepository = pRFrameworkRepository;
        _configuration = configuration;
        _defaultLocale = _configuration.GetValue<string>("DefaultLocale") ?? string.Empty;
    }

    public override async Task<SingleResult<PrFramework>> Execute(ByIdPrFrameworkQuery request)
    {
        if (string.IsNullOrEmpty(request.Locale))
        {
            request.Locale = _defaultLocale;
        }

        PrFramework framework = await _pRFrameworkRepository.GetByIdAndLocale(request.Id, request.Locale);

        IQueryable<PrFramework> result = framework is not null
             ? new List<PrFramework> { framework }.AsQueryable()
             : Enumerable.Empty<PrFramework>().AsQueryable();

        return SingleResult.Create(result);
    }
}
