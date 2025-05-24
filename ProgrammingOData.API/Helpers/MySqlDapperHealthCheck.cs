using Dapper;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MySql.Data.MySqlClient;

namespace ProgrammingOData.API.Helpers;

public class MySqlDapperHealthCheck : IHealthCheck
{
    private readonly string _connectionString;

    public MySqlDapperHealthCheck(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);

            int result = await connection.ExecuteScalarAsync<int>("SELECT 1");
            return result == 1
                ? HealthCheckResult.Healthy("MySQL Dapper is healthy.")
                : HealthCheckResult.Unhealthy("Unexpected result from DB.");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("MySQL Dapper health check failed.", ex);
        }
    }
}