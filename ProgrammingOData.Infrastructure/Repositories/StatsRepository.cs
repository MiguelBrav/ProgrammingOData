using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;
using ProgrammingOData.Models.Models;


namespace ProgrammingOData.Infrastructure.Repositories;

public class StatsRepository : IStatsRepository
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public StatsRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("MySql") ?? string.Empty;
    }

    public async Task<GlobalStatsResponse> GetGlobalStats()
    {
        using var connection = new MySqlConnection(_connectionString);

        var sql = @"
        SELECT
            (SELECT COUNT(*) FROM prframeworks) AS TotalFrameworks,
            (SELECT COUNT(DISTINCT FrameworkId) FROM prframeworkdescriptions) AS FrameworksWithDescriptions,
            (SELECT COUNT(*) FROM prlanguages) AS TotalLanguages,
            (SELECT COUNT(*) FROM prframeworkdescriptions) AS TotalDescriptions,
            (SELECT GROUP_CONCAT(DISTINCT Locale) FROM prframeworkdescriptions) AS Locales;
    ";

        var result = await connection.QueryFirstAsync<GlobalStatsRaw>(sql);

        return new GlobalStatsResponse
        {
            Frameworks = new FrameworkStats
            {
                Total = (int)result.TotalFrameworks,
                WithDescriptions = (int)result.FrameworksWithDescriptions
            },
            Languages = (int)result.TotalLanguages,
            Descriptions = new DescriptionStats
            {
                Total = (int)result.TotalDescriptions,
                Locales = result.Locales != null
                   ? result.Locales.Split(',').ToList()
                   : new List<string>()
            }
        };
    }

}
