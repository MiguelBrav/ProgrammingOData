using MediatR;
using ProgrammingOData.Models.Models;

namespace ProgrammingOData.API.Queries.Admin;

public class GlobalStatsQuery : IRequest<GlobalStatsResponse>
{
}
