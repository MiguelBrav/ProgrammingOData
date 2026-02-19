using UseCaseCore.UseCases;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.API.Helpers;
using ProgrammingOData.API.Helpers.Enums;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Commands;

public class CreateUserCommandHandler : UseCaseBase<CreateUserCommand, IActionResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleUserRepository _roleUserRepository;
    private readonly IConfiguration _configuration;
    private readonly string _hashKey;

    public CreateUserCommandHandler(IUserRepository userRepository, IRoleUserRepository roleUserRepository, 
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _roleUserRepository = roleUserRepository;
        _configuration = configuration;
        _hashKey = _configuration.GetValue<string>("Hashkey") ?? string.Empty;

    }

    public override async Task<IActionResult> Execute(CreateUserCommand request)
    {
        User existsUser = await _userRepository.GetByEmail(request.createUser.Email);

        if (existsUser is not null)
        {
            return new BadRequestObjectResult(new
            {
                Message = $"User with email {request.createUser.Email} already exists"
            });
        }
        Hashing hasher = new Hashing();

        string _hashPsw = hasher.HashPasswordWithHMACSHA256(request.createUser.Password,_hashKey);

        User user = new User(request.createUser.Email, _hashPsw,
            request.createUser.UserName, request.createUser.DateOfBirth);

        RoleUser userRole = new RoleUser(user.UserId, nameof(UserRole.Normal));

        try
        {
            Guid _newUserId = await _userRepository.Create(user);

            await _roleUserRepository.Create(userRole);

            var newResourceUri = $"/users/{_newUserId}";
            return new CreatedResult(newResourceUri, new
            {
                CreatedAt = DateTime.UtcNow,  
                Location = newResourceUri     
            });
        }
        catch (Exception)
        {
            return new BadRequestObjectResult(new
            {
                Message = $"User fail to be created"
            });
        }     
    }
}
