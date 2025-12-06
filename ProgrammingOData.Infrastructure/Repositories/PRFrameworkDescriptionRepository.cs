using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;


namespace ProgrammingOData.Infrastructure.Repositories;

public class PRFrameworkDescriptionRepository : IPRFrameworkDescriptionRepository
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public PRFrameworkDescriptionRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("MySql") ?? string.Empty;
    }

    public async Task<int> CountByLanguage(int frameworkId)
    {
        using var connection = new MySqlConnection(_connectionString);
        var query = "SELECT COUNT(*) FROM prframeworkdescriptions WHERE FrameworkId = @FrameworkId";
        var count = await connection.ExecuteScalarAsync<int>(query, new { FrameworkId = frameworkId });
        return count;
    }
    public async Task<int> CountByLanguageLocale(int frameworkId, string locale)
    {
        using var connection = new MySqlConnection(_connectionString);
        var query = "SELECT COUNT(*) FROM prframeworkdescriptions WHERE FrameworkId = @FrameworkId And Locale = @Locale";
        var count = await connection.ExecuteScalarAsync<int>(query, new { FrameworkId = frameworkId, Locale = locale });
        return count;
    }

    public async Task<int> Create(PrFrameworkDescription frameworkDescription)
    {
        using var connection = new MySqlConnection(_connectionString);
        var query = @"
        INSERT INTO prframeworkdescriptions (FrameworkId, Locale, Description)
        VALUES (@FrameworkId, @Locale, @Description);
        SELECT LAST_INSERT_ID();";

        var id = await connection.ExecuteScalarAsync<int>(query, new
        {
            frameworkDescription.FrameworkId,
            frameworkDescription.Locale,
            frameworkDescription.Description
        });
        return id;
    }

    public async Task<PrFrameworkDescription> GetById(int id)
    {
        using var connection = new MySqlConnection(_connectionString);

        var sql = @"
        SELECT Id, FrameworkId, Locale, Description
        FROM prframeworkdescriptions
        WHERE Id = @Id;
        ";

        var frameworkDesc = await connection.QueryFirstOrDefaultAsync<PrFrameworkDescription>(sql, new { Id = id });
        return frameworkDesc;
    }

    public async Task Update(PrFrameworkDescription frameworkDescription)
    {
        using var connection = new MySqlConnection(_connectionString);

        var sql = @" UPDATE prframeworkdescriptions
        SET FrameworkId = @FrameworkId, Locale = @Locale, Description = @Description
        WHERE Id = @Id;";

        await connection.ExecuteAsync(sql, new
        {
            frameworkDescription.Id,
            frameworkDescription.FrameworkId,
            frameworkDescription.Locale,
            frameworkDescription.Description
        });
    }

}
