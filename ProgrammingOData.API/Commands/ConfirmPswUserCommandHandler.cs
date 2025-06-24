using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.API.Helpers;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;
using System.Security.Claims;

namespace ProgrammingOData.API.Commands;

public class ConfirmPswUserCommandHandler : IRequestHandler<ConfirmPswUserCommand, IActionResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly string _hashKey;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public ConfirmPswUserCommandHandler(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _hashKey = _configuration.GetValue<string>("Hashkey") ?? string.Empty;

    }

    public async Task<IActionResult> Handle(ConfirmPswUserCommand request, CancellationToken cancellationToken)
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

        string _userId = await _userRepository.GetValidResetUserId(request.confirmPsw.Token);

        if (string.IsNullOrEmpty(_userId))
        {
            return new BadRequestObjectResult(new
            {
                Message = $"Token reset is not valid"
            });
        }
        try
        {
            Hashing hasher = new Hashing();

            string _hashPsw = hasher.HashPasswordWithHMACSHA256(request.confirmPsw.ConfirmPsw, _hashKey);

           await _userRepository.ConfirmPsw(existsUser.UserId.ToString(),request.confirmPsw.Token,_hashPsw);

            return new OkObjectResult(new
            {
                Message = "Password updated successfully"
            });
        }
        catch (Exception)
        {
            return new BadRequestObjectResult(new
            {
                Message = $"Password fail to be confirmed"
            });
        }     
    }
}
