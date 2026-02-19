using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Queries;

public class AllSupportedLocalesQueryHandler : UseCaseBase<AllSupportedLocalesQuery, IQueryable<SupportedLocale>>
{
    private readonly ISupportedLocaleRepository _supportedLocaleRepository;

    public AllSupportedLocalesQueryHandler(ISupportedLocaleRepository supportedLocaleRepository)
    {
        _supportedLocaleRepository = supportedLocaleRepository;
    }

    public override async Task<IQueryable<SupportedLocale>> Execute(AllSupportedLocalesQuery request)
    {
        List<SupportedLocale> locales = await _supportedLocaleRepository.GetAll() ?? new List<SupportedLocale>();

        return locales.AsQueryable();
    }
}
