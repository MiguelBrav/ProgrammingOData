using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;
using ProgrammingOData.Models.Models;

namespace ProgrammingOData.Infrastructure.Repositories;

public class SupportedLocaleRepository : ISupportedLocaleRepository
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public SupportedLocaleRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("MySql") ?? string.Empty;
    }

    public async Task<int> Create(SupportedLocale locale)
    {
        using var connection = new MySqlConnection(_connectionString);
        var query = @"
        INSERT INTO supportedlocales (Locale, Name, IsActive)
        VALUES (@Locale, @Name, @IsActive);
        SELECT LAST_INSERT_ID();";

        locale.Id = await connection.ExecuteScalarAsync<int>(query, new
        {
            locale.Locale,
            locale.Name,
            locale.IsActive
        });

        return locale.Id;
    }

    public async Task<List<SupportedLocale>> GetAll()
    {
        // To - Do - replace query for store procedure
        using var connection = new MySqlConnection(_connectionString);
        var query = @"SELECT Id, Locale, Name, IsActive FROM supportedlocales where IsActive = true";
        var locales = await connection.QueryAsync<SupportedLocale>(query);
        return locales.ToList();
    }

    public async Task<SupportedLocale> GetById(int id)
    {
        // To - Do - replace query for store procedure
        using var connection = new MySqlConnection(_connectionString);
        var query = "SELECT Id, Locale, Name, IsActive FROM supportedlocales where IsActive = true and Id = @Id";
        var locale = await connection.QueryFirstOrDefaultAsync<SupportedLocale>(query, new { Id = id });
        return locale;
    }

    public async Task<SupportedLocale> GetByLocale(string param)
    {
        // To - Do - replace query for store procedure
        using var connection = new MySqlConnection(_connectionString);
        var query = "SELECT Id, Locale, Name, IsActive FROM supportedlocales where Locale = @Locale";
        var locale = await connection.QueryFirstOrDefaultAsync<SupportedLocale>(query, new { Locale = param });
        return locale;
    }

    public async Task SetLocaleActiveStatus(int id, bool isActive)
    {
        // To - Do - replace query for store procedure
        using var connection = new MySqlConnection(_connectionString);
        var query = "UPDATE supportedlocales SET IsActive = @IsActive WHERE Id = @Id";
        await connection.ExecuteAsync(query, new { Id = id, IsActive = isActive });
    }

    public async Task Update(SupportedLocale locale)
    {
        using var connection = new MySqlConnection(_connectionString);
        var query = @"
        UPDATE supportedlocales
        SET Locale = @Locale,
            Name = @Name,
            IsActive = @IsActive
        WHERE Id = @Id";

        await connection.ExecuteAsync(query, new
        {
            locale.Id,
            locale.Locale,
            locale.Name,
            locale.IsActive
        });
    }
}
