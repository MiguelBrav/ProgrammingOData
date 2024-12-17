using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public UserRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("MySql") ?? string.Empty;
    }

    public async Task<Guid> Create(User user)
    {
        using var connection = new MySqlConnection(_connectionString);
        // To - Do - replace query for store procedure
        var query = @"
        INSERT INTO users (UserId, Email, EmailNormalized, UserName, DateOfBirth, Password)
        VALUES (@UserId, @Email, @EmailNormalized, @UserName, @DateOfBirth, @Password);";

        await connection.ExecuteAsync(query, user);

        return user.UserId; 
    }

    public async Task<User> GetByEmail(string email)
    {
        // To - Do - replace query for store procedure
        using var connection = new MySqlConnection(_connectionString);
        var query = "SELECT UserId, Email, EmailNormalized, UserName, DateOfBirth, Password FROM users WHERE Email = @Email";
        var user = await connection.QueryFirstOrDefaultAsync<User>(query, new { Email = email });
        return user;
    }

    public async Task<UserWithRol> ValidateUserAndRole(string email)
    {
        using var connection = new MySqlConnection(_connectionString);
        var query = @"SELECT u.UserId, u.Email, u.Password, ur.UserRole
                    FROM users u
                    INNER JOIN usersrole ur ON u.UserId = ur.UserId
                    WHERE Email = @Email";
        var user = await connection.QueryFirstOrDefaultAsync<UserWithRol>(query, new { Email = email });
        return user;
    }
}
