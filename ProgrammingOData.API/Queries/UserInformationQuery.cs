using MediatR;
using Microsoft.AspNetCore.OData.Results;
using ProgrammingOData.Models.Models;

namespace ProgrammingOData.API.Queries;
public class UserInformationQuery : IRequest<SingleResult<UserInformation>>
{
}
