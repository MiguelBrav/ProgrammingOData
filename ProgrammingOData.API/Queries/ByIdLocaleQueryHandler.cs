using Microsoft.AspNetCore.OData.Results;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Queries;

public class ByIdLocaleQueryHandler : UseCaseBase<ByIdLocaleQuery, SingleResult<SupportedLocale>>
{
    private readonly ISupportedLocaleRepository _supportedLocaleRepository;

    public ByIdLocaleQueryHandler(ISupportedLocaleRepository supportedLocaleRepository)
    {
        _supportedLocaleRepository = supportedLocaleRepository;
    }

    public override async Task<SingleResult<SupportedLocale>> Execute(ByIdLocaleQuery request)
    {

        SupportedLocale locale = await _supportedLocaleRepository.GetById(request.Id);

        IQueryable<SupportedLocale> result = locale is not null
             ? new List<SupportedLocale> { locale }.AsQueryable()
             : Enumerable.Empty<SupportedLocale>().AsQueryable();

        return SingleResult.Create(result);
    }
}
