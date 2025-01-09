using MediatR;
using ProgrammingOData.Models.Models;

namespace ProgrammingOData.API.Queries;
public class AllUsersQuery : IRequest<IQueryable<UserRoleDashboard>>
{
}
