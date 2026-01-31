using MediatR;
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
        private IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EnableQuery]
        [HttpGet]
        [ServiceFilter(typeof(BasicAdminAuthFilter))]
        public async Task<IActionResult> Get()
        {
            try
            {
                AllUsersQuery allUsersQuery = new AllUsersQuery();

                IQueryable<UserRoleDashboard> users = await _mediator.Send(allUsersQuery);

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

                SingleResult<UserRoleDashboard> user = await _mediator.Send(userQuery);

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

                SingleResult<UserInformation> users = await _mediator.Send(userInformationQuery);

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

                return await _mediator.Send(userCommand);
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

                return await _mediator.Send(loginUserCommand);
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

                return await _mediator.Send(userToAdminCommand);
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

                return await _mediator.Send(changePswUserCommand);
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
                
                return await _mediator.Send(confirmPswUserCommand);
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

                var result = await _mediator.Send(globalQuery);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }
    }
}
