using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.API.Helpers.Enums;
using ProgrammingOData.Models.Models;

namespace ProgrammingOData.API.Helpers;

public class BasicAdminAuthFilter : Attribute, IAsyncAuthorizationFilter
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly string _hashKey;

    public BasicAdminAuthFilter(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _hashKey = _configuration.GetValue<string>("Hashkey") ?? string.Empty;
    }
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!authHeader.ToString().StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        string encodedCredentials = authHeader.ToString().Substring("Basic ".Length).Trim();
        string[] decodedCredentials;
        try
        {
            string decodedString = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
            decodedCredentials = decodedString.Split(':');
        }
        catch
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (decodedCredentials.Length != 2)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        string userEmail = decodedCredentials[0];
        string password = decodedCredentials[1];

        UserWithRol existsUser = await _userRepository.ValidateUserAndRole(userEmail);

        if (existsUser is null)
        {
            context.Result = new UnauthorizedObjectResult(new
            {
                Message = "User does not exists"
            });
            return;
        }

        Hashing hasher = new Hashing();

        string _hashPsw = hasher.HashPasswordWithHMACSHA256(password, _hashKey);

        if (existsUser.Password != _hashPsw)
        {
            context.Result = new UnauthorizedObjectResult(new
            {
                Message = "Invalid password"
            });
            return;
        }

        if (existsUser.UserRole != nameof(UserRole.Admin))
        {
            context.Result = new UnauthorizedObjectResult(new
            {
                Message = "User is not allowed to manage data"
            });
            return;
        }

    }
}
