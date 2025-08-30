using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.Domain.Interfaces;

public interface IPRFrameworkRepository
{
    Task<List<PrFramework>> GetAll();
    Task<List<PrFramework>> GetAllByLocale(string locale);
    Task<int> Create(PrFramework framework);
    Task<PrFramework> GetById(int id);
    Task<PrFramework> GetByIdAndLocale(int id, string locale);
}
