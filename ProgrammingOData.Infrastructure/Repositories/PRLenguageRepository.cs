using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.Infrastructure.Repositories;

public class PRLenguageRepository : IPRLanguageRepository
{
    private readonly IConfiguration configuration;
    private readonly string _connectionString;

    public PRLenguageRepository(IConfiguration _configuration)
    {
        configuration = _configuration;
        _connectionString = configuration.GetConnectionString("MySql") ?? string.Empty;
    }

    public async Task<List<PrLanguage>> GetAll()
    {
        // To - Do - replace query for store procedure
        using var connection = new MySqlConnection(_connectionString);
        var query = "SELECT Id, Name, YearCreated, Creator FROM prlanguages";
        var languages = await connection.QueryAsync<PrLanguage>(query);
        return languages.ToList();
    }
}
