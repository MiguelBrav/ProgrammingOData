using MediatR;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Queries;

public class AllShortyQueryHandler : IRequestHandler<AllPrLanguageQuery, IQueryable<PrLanguage>>
{
    private readonly IPRLanguageRepository _prLanguageRepository;

    public AllShortyQueryHandler(IPRLanguageRepository prLanguageRepository)
    {
        _prLanguageRepository = prLanguageRepository;
    }

    public async Task<IQueryable<PrLanguage>> Handle(AllPrLanguageQuery request, CancellationToken cancellationToken)
    {
        List<PrLanguage> languages = await _prLanguageRepository.GetAll() ?? new List<PrLanguage>();

        return languages.AsQueryable();
    }
}
