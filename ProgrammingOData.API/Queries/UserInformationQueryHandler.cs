using MediatR;
using Microsoft.AspNetCore.OData.Results;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Infrastructure.Repositories;
using ProgrammingOData.Models.Entities;
using ProgrammingOData.Models.Models;
using System.Security.Claims;

namespace ProgrammingOData.API.Queries;

public class UserInformationQueryHandler : IRequestHandler<UserInformationQuery, SingleResult<UserInformation>>
{
    private readonly IUserRepository _userRepository;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserInformationQueryHandler(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _httpContextAccessor=httpContextAccessor;
    }

    public async Task<SingleResult<UserInformation>> Handle(UserInformationQuery request, CancellationToken cancellationToken)
    {
        var email = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;

        UserInformation userInformation = await _userRepository.GetInformacion(email);

        IQueryable<UserInformation> result = userInformation is not null
             ? new List<UserInformation> { userInformation }.AsQueryable()
             : Enumerable.Empty<UserInformation>().AsQueryable();

        return SingleResult.Create(result);
    }
}
