using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;


namespace ProgrammingOData.Infrastructure.Repositories;

public class PRLanguageDescriptionRepository : IPRLanguageDescriptionRepository
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public PRLanguageDescriptionRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("MySql") ?? string.Empty;
    }

    public async Task<int> CountByLanguage(int languageId)
    {
        using var connection = new MySqlConnection(_connectionString);
        var query = "SELECT COUNT(*) FROM prlanguagedescriptions WHERE LanguageId = @LanguageId";
        var count = await connection.ExecuteScalarAsync<int>(query, new { LanguageId = languageId });
        return count;
    }

    public async Task<List<PrLanguageDescription>> GetAll()
    {
        // To - Do - replace query for store procedure
        using var connection = new MySqlConnection(_connectionString);
        var query = "SELECT Id, LanguageId, Locale, Description FROM prlanguagedescriptions";
        var languagesDescriptions = await connection.QueryAsync<PrLanguageDescription>(query);
        return languagesDescriptions.ToList();
    }

    public async Task<PrLanguageDescription> GetById(int id)
    {
        // To - Do - replace query for store procedure
        using var connection = new MySqlConnection(_connectionString);
        var query = "SELECT Id, LanguageId, Locale, Description FROM prlanguagedescriptions WHERE Id = @Id";
        var language = await connection.QueryFirstOrDefaultAsync<PrLanguageDescription>(query, new { Id = id });
        return language;
    }
}
