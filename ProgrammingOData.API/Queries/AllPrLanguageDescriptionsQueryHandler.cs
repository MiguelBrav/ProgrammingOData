using MediatR;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Queries;

public class AllPrLanguageDescriptionsQueryHandler : IRequestHandler<AllPrLanguageDescriptionsQuery, IQueryable<PrLanguageDescription>>
{
    private readonly IPRLanguageDescriptionRepository _prLanguageDescriptionRepository;


    public AllPrLanguageDescriptionsQueryHandler(IPRLanguageDescriptionRepository prLanguageDescriptionRepository)
    {
        _prLanguageDescriptionRepository = prLanguageDescriptionRepository;
    }

    public async Task<IQueryable<PrLanguageDescription>> Handle(AllPrLanguageDescriptionsQuery request, CancellationToken cancellationToken)
    {

        List<PrLanguageDescription> languages = await _prLanguageDescriptionRepository.GetAll() ?? new List<PrLanguageDescription>();

        return languages.AsQueryable();
    }
}
