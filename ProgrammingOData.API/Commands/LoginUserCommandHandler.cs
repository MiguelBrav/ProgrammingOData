using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.API.Helpers;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;
using System.Text;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Commands;

public class LoginUserCommandHandler : UseCaseBase<LoginUserCommand, IActionResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly string _hashKey;

    public LoginUserCommandHandler(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _hashKey = _configuration.GetValue<string>("Hashkey") ?? string.Empty;

    }

    public override async Task<IActionResult> Execute(LoginUserCommand request)
    {
        User existsUser = await _userRepository.GetByEmail(request.loginUser.Email);

        if (existsUser is null)
        {
            return new BadRequestObjectResult(new
            {
                Message = $"User with email {request.loginUser.Email} does not exists"
            });
        }

        Hashing hasher = new Hashing();

        string _hashPsw = hasher.HashPasswordWithHMACSHA256(request.loginUser.Password, _hashKey);

        if (existsUser.Password != _hashPsw)
        {
            return new BadRequestObjectResult(new
            {
                Message = "Invalid password"
            });
        }

        try
        {
            string credentials = $"{request.loginUser.Email}:{request.loginUser.Password}";
            string basicToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));

            return new OkObjectResult(new
            {
                Message = "Login successful",
                Token = $"Basic {basicToken}" 
            });
        }
        catch (Exception)
        {
            return new BadRequestObjectResult(new
            {
                Message = $"User failed to log in"
            });
        }
    }
}
