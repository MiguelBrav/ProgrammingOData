using ProgrammingOData.API.Commands;
using ProgrammingOData.API.Queries;
using ProgrammingOData.API.Queries.Admin;
using ProgrammingOData.Models.Models;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.API.Aggregator.Interfaces;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Aggregator;

public class UsersAggregator : IUsersAggregator
{
    private readonly UseCaseDispatcher _dispatcher;
    private readonly AllUsersQueryHandler _allUsersHandler;
    private readonly ByIdUserQueryHandler _byIdUserHandler;
    private readonly UserInformationQueryHandler _userInfoHandler;
    private readonly CreateUserCommandHandler _createUserHandler;
    private readonly LoginUserCommandHandler _loginUserHandler;
    private readonly UserRoleCommandHandler _userRoleHandler;
    private readonly ChangePswUserCommandHandler _changePswHandler;
    private readonly ConfirmPswUserCommandHandler _confirmPswHandler;
    private readonly GlobalStatsQueryHandler _globalStatsHandler;

    public UsersAggregator(
        UseCaseDispatcher dispatcher,
        AllUsersQueryHandler allUsersHandler,
        ByIdUserQueryHandler byIdUserHandler,
        UserInformationQueryHandler userInfoHandler,
        CreateUserCommandHandler createUserHandler,
        LoginUserCommandHandler loginUserHandler,
        UserRoleCommandHandler userRoleHandler,
        ChangePswUserCommandHandler changePswHandler,
        ConfirmPswUserCommandHandler confirmPswHandler,
        GlobalStatsQueryHandler globalStatsHandler)
    {
        _dispatcher = dispatcher;
        _allUsersHandler = allUsersHandler;
        _byIdUserHandler = byIdUserHandler;
        _userInfoHandler = userInfoHandler;
        _createUserHandler = createUserHandler;
        _loginUserHandler = loginUserHandler;
        _userRoleHandler = userRoleHandler;
        _changePswHandler = changePswHandler;
        _confirmPswHandler = confirmPswHandler;
        _globalStatsHandler = globalStatsHandler;
    }

    public async Task<IQueryable<UserRoleDashboard>> AllUsersQuery(AllUsersQuery request)
        => await _dispatcher.Dispatch(_allUsersHandler, request);

    public async Task<SingleResult<UserRoleDashboard>> ByIdUserQuery(ByIdUserQuery request)
        => await _dispatcher.Dispatch(_byIdUserHandler, request);

    public async Task<SingleResult<UserInformation>> UserInformationQuery(UserInformationQuery request)
        => await _dispatcher.Dispatch(_userInfoHandler, request);

    public async Task<IActionResult> CreateUser(CreateUserCommand request)
        => await _dispatcher.Dispatch(_createUserHandler, request);

    public async Task<IActionResult> LoginUser(LoginUserCommand request)
        => await _dispatcher.Dispatch(_loginUserHandler, request);

    public async Task<IActionResult> UpdateUserRole(UserRoleCommand request)
        => await _dispatcher.Dispatch(_userRoleHandler, request);

    public async Task<IActionResult> ChangePassword(ChangePswUserCommand request)
        => await _dispatcher.Dispatch(_changePswHandler, request);

    public async Task<IActionResult> ConfirmPassword(ConfirmPswUserCommand request)
        => await _dispatcher.Dispatch(_confirmPswHandler, request);

    public async Task<GlobalStatsResponse> GlobalStats(GlobalStatsQuery request)
        => await _dispatcher.Dispatch(_globalStatsHandler, request);
}
