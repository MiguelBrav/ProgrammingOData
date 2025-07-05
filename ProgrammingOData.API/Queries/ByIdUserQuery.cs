using MediatR;
using Microsoft.AspNetCore.OData.Results;
using ProgrammingOData.Models.Entities;
using ProgrammingOData.Models.Models;

namespace ProgrammingOData.API.Queries;
public class ByIdUserQuery : IRequest<SingleResult<UserRoleDashboard>>
{
    public string Id { get; set; } = string.Empty;
}
