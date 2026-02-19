using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Models;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Queries.Admin;

public class GlobalStatsQueryHandler : UseCaseBase<GlobalStatsQuery, GlobalStatsResponse>
{
    private readonly IStatsRepository _statsRepositoty;


    public GlobalStatsQueryHandler(IStatsRepository statsRepository)
    {
        _statsRepositoty = statsRepository;
    }

    public override async Task<GlobalStatsResponse> Execute(GlobalStatsQuery request)
    {

        GlobalStatsResponse globalStats = await _statsRepositoty.GetGlobalStats() ?? new GlobalStatsResponse();

        return globalStats;
    }
}