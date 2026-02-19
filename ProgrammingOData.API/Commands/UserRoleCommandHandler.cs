using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.API.Helpers.Enums;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Commands;

public class UserRoleCommandHandler : UseCaseBase<UserRoleCommand, IActionResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleUserRepository _roleUserRepository;
    private readonly IConfiguration _configuration;
    private readonly string _pswToAdmin;

    public UserRoleCommandHandler(IUserRepository userRepository, IRoleUserRepository roleUserRepository,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _roleUserRepository = roleUserRepository;
        _configuration = configuration;
        _pswToAdmin = _configuration.GetValue<string>("PswToAdmin") ?? string.Empty;
    }

    public override async Task<IActionResult> Execute(UserRoleCommand request)
    {
        User existsUser = await _userRepository.GetByEmail(request.userWithRol.Email);

        if (existsUser is null)
        {
            return new BadRequestObjectResult(new
            {
                Message = $"User with email {request.userWithRol.Email} does not exists"
            });
        }

        if (_pswToAdmin != request.userWithRol.Password?.Trim())
        {
            return new UnauthorizedObjectResult(new
            {
                Message = "Not allowed to update UserRole"
            });
        }

        if (!Enum.TryParse(typeof(UserRole), request.userWithRol.UserRole, ignoreCase: true, out object? parsedRole))
        {
            return new BadRequestObjectResult(new
            {
                Message = $"Invalid UserRole: {request.userWithRol.UserRole}"
            });
        }

        RoleUser userRole = new RoleUser(existsUser.UserId, parsedRole.ToString()!);

        try
        {
            await _roleUserRepository.Update(userRole);

            return new OkObjectResult(new
            {
                Message = "UserRole is updated"
            });
        }
        catch (Exception)
        {
            return new BadRequestObjectResult(new
            {
                Message = "UserRole fail to be updated"
            });
        }
    }
}
