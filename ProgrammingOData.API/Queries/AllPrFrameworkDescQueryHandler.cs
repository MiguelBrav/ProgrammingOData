using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Queries;

public class AllPrFrameworkDescQueryHandler : UseCaseBase<AllPrFrameworkDescQuery, IQueryable<PrFrameworkDescription>>
{
    private readonly IPRFrameworkDescriptionRepository _pRFrameworkDescriptionRepository;


    public AllPrFrameworkDescQueryHandler(IPRFrameworkDescriptionRepository pRFrameworkDescriptionRepository)
    {
        _pRFrameworkDescriptionRepository = pRFrameworkDescriptionRepository;
    }

    public override async Task<IQueryable<PrFrameworkDescription>> Execute(AllPrFrameworkDescQuery request)
    {

        List<PrFrameworkDescription> frameworksDesc = await _pRFrameworkDescriptionRepository.GetAll() ?? new List<PrFrameworkDescription>();

        return frameworksDesc.AsQueryable();
    }
}
