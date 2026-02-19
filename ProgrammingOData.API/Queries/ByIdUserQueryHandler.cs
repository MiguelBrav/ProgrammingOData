using Microsoft.AspNetCore.OData.Results;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Models;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Queries;

public class ByIdUserQueryHandler : UseCaseBase<ByIdUserQuery, SingleResult<UserRoleDashboard>>
{
    private readonly IUserRepository _userRepository;
        public ByIdUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public override async Task<SingleResult<UserRoleDashboard>> Execute(ByIdUserQuery request)
    {
        UserRoleDashboard user = await _userRepository.GetUserDashById(request.Id);

        IQueryable<UserRoleDashboard> result = user is not null
             ? new List<UserRoleDashboard> { user }.AsQueryable()
             : Enumerable.Empty<UserRoleDashboard>().AsQueryable();

        return SingleResult.Create(result);
    }
}
