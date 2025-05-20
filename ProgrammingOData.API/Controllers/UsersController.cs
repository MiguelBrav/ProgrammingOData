using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ProgrammingOData.API.Commands;
using ProgrammingOData.API.Helpers;
using ProgrammingOData.API.Queries;
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
    }
}
