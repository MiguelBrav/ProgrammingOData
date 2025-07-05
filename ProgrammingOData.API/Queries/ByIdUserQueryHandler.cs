using MediatR;
using Microsoft.AspNetCore.OData.Results;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Entities;
using ProgrammingOData.Models.Models;
using System.Configuration;

namespace ProgrammingOData.API.Queries;

public class ByIdUserQueryHandler : IRequestHandler<ByIdUserQuery, SingleResult<UserRoleDashboard>>
{
    private readonly IUserRepository _userRepository;
        public ByIdUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<SingleResult<UserRoleDashboard>> Handle(ByIdUserQuery request, CancellationToken cancellationToken)
    {
        UserRoleDashboard user = await _userRepository.GetUserDashById(request.Id);

        IQueryable<UserRoleDashboard> result = user is not null
             ? new List<UserRoleDashboard> { user }.AsQueryable()
             : Enumerable.Empty<UserRoleDashboard>().AsQueryable();

        return SingleResult.Create(result);
    }
}
