using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.Infrastructure.Repositories;

public class RoleUserRepository : IRoleUserRepository
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public RoleUserRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("MySql") ?? string.Empty;
    }

    public async Task Create(RoleUser roleUser)
    {
        using var connection = new MySqlConnection(_connectionString);
        // To - Do - replace query for store procedure
        var query = @"
        INSERT INTO usersrole (UserId, UserRole)
        VALUES (@UserId, @UserRole);";

        await connection.ExecuteAsync(query, roleUser); 
    }

}
