using MediatR;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Queries;

public class AllSupportedLocalesQueryHandler : IRequestHandler<AllSupportedLocalesQuery, IQueryable<SupportedLocale>>
{
    private readonly ISupportedLocaleRepository _supportedLocaleRepository;

    public AllSupportedLocalesQueryHandler(ISupportedLocaleRepository supportedLocaleRepository)
    {
        _supportedLocaleRepository = supportedLocaleRepository;
    }

    public async Task<IQueryable<SupportedLocale>> Handle(AllSupportedLocalesQuery request, CancellationToken cancellationToken)
    {
        List<SupportedLocale> locales = await _supportedLocaleRepository.GetAll() ?? new List<SupportedLocale>();

        return locales.AsQueryable();
    }
}
