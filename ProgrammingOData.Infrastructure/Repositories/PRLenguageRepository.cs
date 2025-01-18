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
    public async Task<List<PrLanguage>> GetAllByLocale(string locale)
    {
        // To - Do - replace query for store procedure
        using var connection = new MySqlConnection(_connectionString);
        var query = @"
        SELECT 
            pl.Id, 
            pl.Name, 
            pl.YearCreated, 
            pl.Creator, 
            COALESCE(pld.Description, '') AS Description
        FROM prlanguages pl
        LEFT JOIN prlanguagedescriptions pld ON pl.Id = pld.LanguageId AND pld.Locale = @Locale
    ";

        var languages = await connection.QueryAsync<PrLanguage>(query, new { Locale = locale });
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
    public async Task<PrLanguage> GetByIdAndLocale(int id, string locale)
    {
        using var connection = new MySqlConnection(_connectionString);
        var query = @"
        SELECT 
            prlanguages.Id, 
            prlanguages.Name, 
            prlanguages.YearCreated, 
            prlanguages.Creator, 
            COALESCE(prlanguagedescriptions.Description, '') AS Description
        FROM prlanguages
        LEFT JOIN prlanguagedescriptions ON prlanguages.Id = prlanguagedescriptions.LanguageId
        AND prlanguagedescriptions.Locale = @Locale
        WHERE prlanguages.Id = @Id
    ";

        var language = await connection.QueryFirstOrDefaultAsync<PrLanguage>(query,
            new { Id = id, Locale = locale });
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
