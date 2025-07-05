using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;
using ProgrammingOData.Models.Models;

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

    public async Task<List<UserRoleDashboard>> GetAll()
    {
        using var connection = new MySqlConnection(_connectionString);
        var query = @"SELECT u.UserId, u.Email, u.UserName, u.DateOfBirth, ur.UserRole
                    FROM users u
                    INNER JOIN usersrole ur ON u.UserId = ur.UserId";
        var users = await connection.QueryAsync<UserRoleDashboard>(query);
        return users.ToList();
    }

    public async Task<UserRoleDashboard> GetUserDashById(string userId)
    {
        using var connection = new MySqlConnection(_connectionString);
        var query = @"SELECT u.UserId, u.Email, u.UserName, u.DateOfBirth, ur.UserRole
                    FROM users u
                    INNER JOIN usersrole ur ON u.UserId = ur.UserId";
        var users = await connection.QueryFirstOrDefaultAsync<UserRoleDashboard>(query);
        return users;
    }

    public async Task<UserInformation> GetInformacion(string email)
    {
        // To - Do - replace query for store procedure
        using var connection = new MySqlConnection(_connectionString);
        var query = @"SELECT u.UserId, u.Email, u.UserName, u.DateOfBirth, ur.UserRole
                    FROM users u
                    INNER JOIN usersrole ur ON u.UserId = ur.UserId
                    WHERE u.Email = @Email";
        var user = await connection.QueryFirstOrDefaultAsync<UserInformation>(query, new { Email = email });
        return user;
    }

    public async Task<string> ChangePsw(string userId)
    {
        // To - Do - replace query for store procedure
        using var connection = new MySqlConnection(_connectionString);
        string? token = Guid.NewGuid().ToString();
        DateTime expiration = DateTime.UtcNow.AddHours(1);
        DateTime createdAt = DateTime.UtcNow;

        var query = @"
        INSERT INTO changepswprocess (UserId, Token, Expiration, CreatedAt, Used)
        VALUES (@UserId, @Token, @Expiration, @CreatedAt, FALSE);";

        await connection.ExecuteAsync(query, new
        {
            UserId = userId,
            Token = token,
            Expiration = expiration,
            CreatedAt = createdAt
        });

        return token;
    }

    public async Task<string> GetValidResetUserId(string token)
    {
        using var connection = new MySqlConnection(_connectionString);

        var query = @"
        SELECT CAST(UserId AS CHAR) 
        FROM changepswprocess
        WHERE Token = @Token 
          AND Used = 0 
          AND Expiration >= NOW();";

        string userId = await connection.QueryFirstOrDefaultAsync<string>(query, new { Token = token });

        return userId ?? string.Empty; 
    }


    public async Task<string> ConfirmPsw(string userId, string token, string newPassword)
    {
        using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        using var transaction = connection.BeginTransaction();

        try
        {
            var updateUserQuery = @"
            UPDATE users
            SET Password = @Password,
                UpdatedAt = NOW()
            WHERE UserId = @UserId;";

            await connection.ExecuteAsync(updateUserQuery, new
            {
                Password = newPassword,
                UserId = userId
            }, transaction);

            var updateTokenQuery = @"
            UPDATE changepswprocess
            SET Used = TRUE,
                UsedDate = NOW()
            WHERE Token = @Token;";

            await connection.ExecuteAsync(updateTokenQuery, new { Token = token }, transaction);

            transaction.Commit();

            return userId;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

}
