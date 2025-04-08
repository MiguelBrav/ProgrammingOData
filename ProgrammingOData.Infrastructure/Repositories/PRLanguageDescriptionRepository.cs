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
    public async Task<int> CountByLanguageLocale(int languageId, string locale)
    {
        using var connection = new MySqlConnection(_connectionString);
        var query = "SELECT COUNT(*) FROM prlanguagedescriptions WHERE LanguageId = @LanguageId And Locale = @Locale";
        var count = await connection.ExecuteScalarAsync<int>(query, new { LanguageId = languageId, Locale = locale });
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

    public async Task<int> Create(PrLanguageDescription languageDescription)
    {
        // To - Do - replace query for store procedure
        using var connection = new MySqlConnection(_connectionString);
        var query = @"
        INSERT INTO prlanguagedescriptions (LanguageId, Locale, Description)
        VALUES (@LanguageId, @Locale, @Description);
        SELECT LAST_INSERT_ID();
        ";

        languageDescription.Id = await connection.ExecuteScalarAsync<int>(query, new
        {
            LanguageId = languageDescription.LanguageId,
            Locale = languageDescription.Locale,
            Description = languageDescription.Description
        });

        return languageDescription.Id;
    }
    public async Task Update(PrLanguageDescription languageDescription)
    {
        using var connection = new MySqlConnection(_connectionString);
        var query = @"
        UPDATE prlanguagedescriptions
        SET LanguageId = @LanguageId, Locale = @Locale, Description = @Description
        WHERE Id = @Id;
        ";

        await connection.ExecuteAsync(query, new
        {
            Id = languageDescription.Id,
            LanguageId = languageDescription.LanguageId,
            Locale = languageDescription.Locale,
            Description = languageDescription.Description
        });

    }

}
