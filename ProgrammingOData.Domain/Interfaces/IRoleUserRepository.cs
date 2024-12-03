using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.Domain.Interfaces;

public interface IRoleUserRepository
{
    Task Create(RoleUser roleUser);
}
