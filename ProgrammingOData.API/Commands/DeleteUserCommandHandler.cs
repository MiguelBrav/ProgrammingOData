using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.Domain.Interfaces;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Commands;

public class DeleteUserCommandHandler : UseCaseBase<DeleteUserCommand, IActionResult>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public override async Task<IActionResult> Execute(DeleteUserCommand request)
    {
        try
        {
            // Perform physical delete of user by UserId (GUID string)
            await _userRepository.Delete(request.UserId);

            return new OkObjectResult(new
            {
                Message = "User deleted successful"
            });
        }
        catch (Exception)
        {
            return new BadRequestObjectResult(new
            {
                Message = "User fail to be deleted"
            });
        }
    }
}
