using ProgrammingOData.Models.Entities;
using ProgrammingOData.Models.Models;

namespace ProgrammingOData.Domain.Interfaces;

public interface IUserRepository
{
    Task<User> GetByEmail(string email);
    Task<Guid> Create(User user);
    Task<UserWithRol> ValidateUserAndRole(string email);
    Task<List<UserRoleDashboard>> GetAll();
    Task<UserRoleDashboard> GetUserDashById(string userId);
    Task<UserInformation> GetInformacion(string email);
    Task<string> ChangePsw(string userId);
    Task<string> GetValidResetUserId(string token);
    Task<string> ConfirmPsw(string userId, string token, string newPassword);

}
