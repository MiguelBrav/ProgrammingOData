using MediatR;
using Microsoft.AspNetCore.OData.Results;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;
using System.Configuration;

namespace ProgrammingOData.API.Queries;

public class ByIdLocaleQueryHandler : IRequestHandler<ByIdLocaleQuery, SingleResult<SupportedLocale>>
{
    private readonly ISupportedLocaleRepository _supportedLocaleRepository;

    public ByIdLocaleQueryHandler(ISupportedLocaleRepository supportedLocaleRepository)
    {
        _supportedLocaleRepository = supportedLocaleRepository;
    }

    public async Task<SingleResult<SupportedLocale>> Handle(ByIdLocaleQuery request, CancellationToken cancellationToken)
    {

        SupportedLocale locale = await _supportedLocaleRepository.GetById(request.Id);

        IQueryable<SupportedLocale> result = locale is not null
             ? new List<SupportedLocale> { locale }.AsQueryable()
             : Enumerable.Empty<SupportedLocale>().AsQueryable();

        return SingleResult.Create(result);
    }
}
