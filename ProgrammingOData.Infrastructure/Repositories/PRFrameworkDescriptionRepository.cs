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
        var query = "SELECT COUNT(*) FROM prframeworkdescriptions WHERE LanguageId = @FrameworkId And Locale = @FrameworkId";
        var count = await connection.ExecuteScalarAsync<int>(query, new { FrameworkId = frameworkId, Locale = locale });
        return count;
    }
}
