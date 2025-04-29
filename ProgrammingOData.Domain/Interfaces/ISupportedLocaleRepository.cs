using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.Domain.Interfaces;

public interface ISupportedLocaleRepository
{
    Task<List<SupportedLocale>> GetAll();
    Task<SupportedLocale> GetById(int id);
    Task<SupportedLocale> GetByLocale(string locale);
    Task<int> Create(SupportedLocale locale);
    Task Update(SupportedLocale locale);
    Task SetLocaleActiveStatus(int id, bool isActive);
}
