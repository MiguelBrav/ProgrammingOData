using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Queries;

public class AllPrLanguageDescriptionsQueryHandler : UseCaseBase<AllPrLanguageDescriptionsQuery, IQueryable<PrLanguageDescription>>
{
    private readonly IPRLanguageDescriptionRepository _prLanguageDescriptionRepository;


    public AllPrLanguageDescriptionsQueryHandler(IPRLanguageDescriptionRepository prLanguageDescriptionRepository)
    {
        _prLanguageDescriptionRepository = prLanguageDescriptionRepository;
    }

    public override async Task<IQueryable<PrLanguageDescription>> Execute(AllPrLanguageDescriptionsQuery request)
    {

        List<PrLanguageDescription> languages = await _prLanguageDescriptionRepository.GetAll() ?? new List<PrLanguageDescription>();

        return languages.AsQueryable();
    }
}
