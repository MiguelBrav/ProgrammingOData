using UseCaseCore.UseCases;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.API.Helpers;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;
using System.Security.Claims;

namespace ProgrammingOData.API.Commands;

public class ChangePswUserCommandHandler : UseCaseBase<ChangePswUserCommand, IActionResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly string _hashKey;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public ChangePswUserCommandHandler(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _hashKey = _configuration.GetValue<string>("Hashkey") ?? string.Empty;

    }

    public override async Task<IActionResult> Execute(ChangePswUserCommand request)
    {
        string email = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;

        User existsUser = await _userRepository.GetByEmail(email);

        if (existsUser is null)
        {
            return new BadRequestObjectResult(new
            {
                Message = $"User does not exists"
            });
        }
        Hashing hasher = new Hashing();

        string _hashPsw = hasher.HashPasswordWithHMACSHA256(request.currentPsw.ConfirmPsw,_hashKey);

        if (_hashPsw != existsUser.Password)
        {
            return new BadRequestObjectResult(new
            {
                Message = $"User not allowed to change password"
            });
        }
        try
        {
            string _resetToken = await _userRepository.ChangePsw(existsUser.UserId.ToString());

            var resourceUri = $"/users/confirm-password/{_resetToken}";
            return new CreatedResult(resourceUri, new
            {
                Token = _resetToken,
                CreatedAt = DateTime.UtcNow,
                Location = resourceUri
            });
        }
        catch (Exception)
        {
            return new BadRequestObjectResult(new
            {
                Message = $"Password fail to be changed"
            });
        }     
    }
}
