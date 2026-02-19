using Microsoft.AspNetCore.OData.Results;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Queries;

public class ByIdPrFrameworkDescriptionQueryHandler : UseCaseBase<ByIdPrFrameworkDescriptionQuery, SingleResult<PrFrameworkDescription>>
{
    private readonly IPRFrameworkDescriptionRepository _pRFrameworkDescRepository;

    private readonly IConfiguration _configuration;

    private string _defaultLocale;

    public ByIdPrFrameworkDescriptionQueryHandler(IPRFrameworkDescriptionRepository pRFrameworkDescRepository, IConfiguration configuration)
    {
        _pRFrameworkDescRepository = pRFrameworkDescRepository;
        _configuration = configuration;
        _defaultLocale = _configuration.GetValue<string>("DefaultLocale") ?? string.Empty;
    }

    public override async Task<SingleResult<PrFrameworkDescription>> Execute(ByIdPrFrameworkDescriptionQuery request)
    {
        if (string.IsNullOrEmpty(request.Locale))
        {
            request.Locale = _defaultLocale;
        }

        PrFrameworkDescription frameworkDesc = await _pRFrameworkDescRepository.GetByIdAndLocale(request.Id, request.Locale);

        IQueryable<PrFrameworkDescription> result = frameworkDesc is not null
             ? new List<PrFrameworkDescription> { frameworkDesc }.AsQueryable()
             : Enumerable.Empty<PrFrameworkDescription>().AsQueryable();

        return SingleResult.Create(result);
    }
}
