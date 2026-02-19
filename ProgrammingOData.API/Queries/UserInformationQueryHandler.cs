using Microsoft.AspNetCore.OData.Results;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Models.Models;
using System.Security.Claims;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Queries;

public class UserInformationQueryHandler : UseCaseBase<UserInformationQuery, SingleResult<UserInformation>>
{
    private readonly IUserRepository _userRepository;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserInformationQueryHandler(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _httpContextAccessor=httpContextAccessor;
    }

    public override async Task<SingleResult<UserInformation>> Execute(UserInformationQuery request)
    {
        var email = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;

        UserInformation userInformation = await _userRepository.GetInformacion(email);

        IQueryable<UserInformation> result = userInformation is not null
             ? new List<UserInformation> { userInformation }.AsQueryable()
             : Enumerable.Empty<UserInformation>().AsQueryable();

        return SingleResult.Create(result);
    }
}
