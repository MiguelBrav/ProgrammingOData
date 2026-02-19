using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;
using ProgrammingOData.Models.Models;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Queries;

public class AllUsersQueryHandler : UseCaseBase<AllUsersQuery, IQueryable<UserRoleDashboard>>
{
    private readonly IUserRepository _userRepository;

    public AllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public override async Task<IQueryable<UserRoleDashboard>> Execute(AllUsersQuery request)
    {
        List<UserRoleDashboard> users = await _userRepository.GetAll() ?? new List<UserRoleDashboard>();

        return users.AsQueryable();
    }
}
