using MediatR;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Queries;

public class AllPrFrameworkDescQueryHandler : IRequestHandler<AllPrFrameworkDescQuery, IQueryable<PrFrameworkDescription>>
{
    private readonly IPRFrameworkDescriptionRepository _pRFrameworkDescriptionRepository;


    public AllPrFrameworkDescQueryHandler(IPRFrameworkDescriptionRepository pRFrameworkDescriptionRepository)
    {
        _pRFrameworkDescriptionRepository = pRFrameworkDescriptionRepository;
    }

    public async Task<IQueryable<PrFrameworkDescription>> Handle(AllPrFrameworkDescQuery request, CancellationToken cancellationToken)
    {

        List<PrFrameworkDescription> frameworksDesc = await _pRFrameworkDescriptionRepository.GetAll() ?? new List<PrFrameworkDescription>();

        return frameworksDesc.AsQueryable();
    }
}
