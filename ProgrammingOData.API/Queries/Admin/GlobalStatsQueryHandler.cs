using MediatR;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Models;

namespace ProgrammingOData.API.Queries.Admin;

public class GlobalStatsQueryHandler : IRequestHandler<GlobalStatsQuery, GlobalStatsResponse>
{
    private readonly IStatsRepository _statsRepositoty;


    public GlobalStatsQueryHandler(IStatsRepository statsRepository)
    {
        _statsRepositoty = statsRepository;
    }

    public async Task<GlobalStatsResponse> Handle(GlobalStatsQuery request, CancellationToken cancellationToken)
    {

        GlobalStatsResponse globalStats = await _statsRepositoty.GetGlobalStats() ?? new GlobalStatsResponse();

        return globalStats;
    }
}