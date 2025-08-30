using MediatR;
using Microsoft.AspNetCore.OData.Results;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Queries;

public class ByIdPrFrameworkQueryHandler : IRequestHandler<ByIdPrFrameworkQuery, SingleResult<PrFramework>>
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

    public async Task<SingleResult<PrFramework>> Handle(ByIdPrFrameworkQuery request, CancellationToken cancellationToken)
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
