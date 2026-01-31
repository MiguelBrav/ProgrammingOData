using ProgrammingOData.Models.Entities;
using ProgrammingOData.Models.Models;

namespace ProgrammingOData.Domain.Interfaces;

public interface IStatsRepository
{
    Task<GlobalStatsResponse> GetGlobalStats();
}
