using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.Domain.Interfaces;

public interface IUserRepository
{
    Task<User> GetByEmail(string email);
    Task<Guid> Create(User user);
    Task<UserWithRol> ValidateUserAndRole(string email);
}
