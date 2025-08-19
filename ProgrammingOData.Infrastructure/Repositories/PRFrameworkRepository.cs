using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.Infrastructure.Repositories;

public class PRFrameworkRepository : IPRFrameworkRepository
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public PRFrameworkRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("MySql") ?? string.Empty;
    }

    public async Task<List<PrFramework>> GetAll()
    {
        using var connection = new MySqlConnection(_connectionString);
        var query = @"
            SELECT 
                Id, 
                Name, 
                LanguageId, 
                CreatedYear, 
                Creator, 
                Description
            FROM prframeworks";
        var frameworks = await connection.QueryAsync<PrFramework>(query);
        return frameworks.ToList();
    }

    public async Task<List<PrFramework>> GetAllByLocale(string locale)
    {
        using var connection = new MySqlConnection(_connectionString);
        var query = @"
            SELECT 
                pf.Id, 
                pf.Name, 
                pf.LanguageId, 
                pf.CreatedYear, 
                pf.Creator, 
                COALESCE(pfd.Description, '') AS Description
            FROM prframeworks pf
            LEFT JOIN prframeworkdescriptions pfd 
                ON pf.Id = pfd.FrameworkId AND pfd.Locale = @Locale";
        var frameworks = await connection.QueryAsync<PrFramework>(query, new { Locale = locale });
        return frameworks.ToList();
    }

    public async Task<int> Create(PrFramework framework)
    {
        using var connection = new MySqlConnection(_connectionString);
        var query = @"
        INSERT INTO prframeworks (Name, LanguageId, CreatedYear, Creator, Description)
        VALUES (@Name, @LanguageId, @CreatedYear, @Creator, @Description);
        SELECT LAST_INSERT_ID();"; 

        var frameworkId = await connection.ExecuteScalarAsync<int>(query, framework);
        return frameworkId;
    }
}
