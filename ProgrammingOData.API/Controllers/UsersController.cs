using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ProgrammingOData.API.Commands;
using ProgrammingOData.API.Helpers;
using ProgrammingOData.API.Queries;
using ProgrammingOData.API.Queries.Admin;
using ProgrammingOData.Models.DTOS;
using ProgrammingOData.Models.Models;

namespace ProgrammingOData.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ODataController
    {
        private readonly Aggregator.Interfaces.IUsersAggregator _aggregator;

        public UsersController(Aggregator.Interfaces.IUsersAggregator aggregator)
        {
            _aggregator = aggregator;
        }

        [EnableQuery]
        [HttpGet]
        [ServiceFilter(typeof(BasicAdminAuthFilter))]
        public async Task<IActionResult> Get()
        {
            try
            {
                AllUsersQuery allUsersQuery = new AllUsersQuery();

                IQueryable<UserRoleDashboard> users = await _aggregator.AllUsersQuery(allUsersQuery);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }


        [EnableQuery]
        [HttpGet("id/{userId}")]
        [ServiceFilter(typeof(BasicAdminAuthFilter))]
        public async Task<IActionResult> GetById(string userId)
        {
            try
            {
                ByIdUserQuery userQuery = new ByIdUserQuery();

                SingleResult<UserRoleDashboard> user = await _aggregator.ByIdUserQuery(userQuery);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }

        [EnableQuery]
        [HttpGet("current-information")]
        [ServiceFilter(typeof(BasicDefaultAuthFilter))]
        public async Task<IActionResult> GetInformation()
        {
            try
            {
                UserInformationQuery userInformationQuery = new UserInformationQuery();

                SingleResult<UserInformation> users = await _aggregator.UserInformationQuery(userInformationQuery);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDTO createUserDTO)
        {
            try
            {
                CreateUserCommand userCommand = new CreateUserCommand
                {
                    createUser = createUserDTO
                };

                return await _aggregator.CreateUser(userCommand);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginUserDTO loginUserDTO)
        {
            try
            {
                LoginUserCommand loginUserCommand = new LoginUserCommand
                {
                    loginUser = loginUserDTO
                };

                return await _aggregator.LoginUser(loginUserCommand);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }

        [HttpPut("role")]
        [ServiceFilter(typeof(BasicAdminAuthFilter))]
        public async Task<IActionResult> UpdateUserRole(UserRoleDTO userRoleDTO)
        {
            try
            {
                UserRoleCommand userToAdminCommand = new UserRoleCommand
                {
                    userWithRol = userRoleDTO
                };

                return await _aggregator.UpdateUserRole(userToAdminCommand);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }

        [HttpPost("change-password")]
        [ServiceFilter(typeof(BasicDefaultAuthFilter))]
        public async Task<IActionResult> ChangePsw(ChangePswDTO changePswDTO)
        {
            try
            {
                ChangePswUserCommand changePswUserCommand = new ChangePswUserCommand
                {
                    currentPsw = changePswDTO
                };

                return await _aggregator.ChangePassword(changePswUserCommand);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }

        [HttpPost("confirm-password/{resetToken}")]
        [ServiceFilter(typeof(BasicDefaultAuthFilter))]
        public async Task<IActionResult> ChangePsw([FromRoute] string resetToken, [FromBody] string newPassword)
        {
            try
            {
                ConfirmPswUserCommand confirmPswUserCommand = new ConfirmPswUserCommand
                {
                    confirmPsw = new ConfirmPswDTO
                    {
                        ConfirmPsw = newPassword,
                        Token = resetToken
                    }
                };
                
                return await _aggregator.ConfirmPassword(confirmPswUserCommand);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }

        [HttpGet("admin/global-stats")]
        [ServiceFilter(typeof(BasicAdminAuthFilter))]
        public async Task<IActionResult> GlobalStats()
        {
            try
            {
                GlobalStatsQuery globalQuery = new GlobalStatsQuery();

                var result = await _aggregator.GlobalStats(globalQuery);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }
    }
}
