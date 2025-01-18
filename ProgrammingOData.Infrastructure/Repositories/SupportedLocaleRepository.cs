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

    public async Task<List<SupportedLocale>> GetAll()
    {
        // To - Do - replace query for store procedure
        using var connection = new MySqlConnection(_connectionString);
        var query = @"SELECT Id, Locale, Name, IsActive FROM supportedlocales where IsActive = true";
        var locales = await connection.QueryAsync<SupportedLocale>(query);
        return locales.ToList();
    }
}
