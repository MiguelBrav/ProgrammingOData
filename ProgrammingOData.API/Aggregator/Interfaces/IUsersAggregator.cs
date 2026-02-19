using ProgrammingOData.API.Commands;
using ProgrammingOData.API.Queries;
using ProgrammingOData.API.Queries.Admin;
using ProgrammingOData.Models.Models;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.Mvc;

namespace ProgrammingOData.API.Aggregator.Interfaces;

public interface IUsersAggregator
{
    Task<IQueryable<UserRoleDashboard>> AllUsersQuery(AllUsersQuery request);
    Task<SingleResult<UserRoleDashboard>> ByIdUserQuery(ByIdUserQuery request);
    Task<SingleResult<UserInformation>> UserInformationQuery(UserInformationQuery request);
    Task<IActionResult> CreateUser(CreateUserCommand request);
    Task<IActionResult> LoginUser(LoginUserCommand request);
    Task<IActionResult> UpdateUserRole(UserRoleCommand request);
    Task<IActionResult> ChangePassword(ChangePswUserCommand request);
    Task<IActionResult> ConfirmPassword(ConfirmPswUserCommand request);
    Task<GlobalStatsResponse> GlobalStats(GlobalStatsQuery request);
}
