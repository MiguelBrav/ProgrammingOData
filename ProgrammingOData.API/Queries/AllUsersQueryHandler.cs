using MediatR;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Models;

namespace ProgrammingOData.API.Queries;

public class AllUsersQueryHandler : IRequestHandler<AllUsersQuery, IQueryable<UserRoleDashboard>>
{
    private readonly IUserRepository _userRepository;

    public AllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IQueryable<UserRoleDashboard>> Handle(AllUsersQuery request, CancellationToken cancellationToken)
    {
        List<UserRoleDashboard> users = await _userRepository.GetAll() ?? new List<UserRoleDashboard>();

        return users.AsQueryable();
    }
}
