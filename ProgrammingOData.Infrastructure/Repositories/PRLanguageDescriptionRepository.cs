﻿using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ProgrammingOData.Domain.Interfaces;


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
}
