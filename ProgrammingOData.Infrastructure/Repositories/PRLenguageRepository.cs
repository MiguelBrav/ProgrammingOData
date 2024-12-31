using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.Infrastructure.Repositories;

public class PRLenguageRepository : IPRLanguageRepository
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public PRLenguageRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("MySql") ?? string.Empty;
    }

    public async Task<List<PrLanguage>> GetAll()
    {
        // To - Do - replace query for store procedure
        using var connection = new MySqlConnection(_connectionString);
        var query = "SELECT Id, Name, YearCreated, Creator FROM prlanguages";
        var languages = await connection.QueryAsync<PrLanguage>(query);
        return languages.ToList();
    }

    public async Task<PrLanguage> GetById(int id)
    {
        // To - Do - replace query for store procedure
        using var connection = new MySqlConnection(_connectionString);
        var query = "SELECT Id, Name, YearCreated, Creator FROM prlanguages WHERE Id = @Id";
        var language = await connection.QueryFirstOrDefaultAsync<PrLanguage>(query, new { Id = id });
        return language;
    }

    public async Task<int> Create(PrLanguage language)
    {
        // To - Do - replace query for store procedure
        using var connection = new MySqlConnection(_connectionString);
        var query = @"
        INSERT INTO prlanguages (Name, YearCreated, Creator)
        VALUES (@Name, @YearCreated, @Creator);
        SELECT LAST_INSERT_ID();
        ";

        language.Id = await connection.ExecuteScalarAsync<int>(query, new
        {
            Name = language.Name,
            YearCreated = language.YearCreated,
            Creator = language.Creator
        });

        return language.Id;
    }

    public async Task Update(PrLanguage language)
    {
        // To - Do - replace query for store procedure
        using var connection = new MySqlConnection(_connectionString);
        var query = @"
        UPDATE prlanguages SET Name = @Name, YearCreated = @YearCreated, Creator = @Creator WHERE Id = @Id; ";

        await connection.ExecuteAsync(query, language);
    }

    public async Task Delete(int id)
    {
        // To - Do - replace query for store procedure
        using var connection = new MySqlConnection(_connectionString);
        var query = @"DELETE FROM prlanguages WHERE Id = @Id; ";

        await connection.ExecuteAsync(query, new { Id = id });
    }
}
