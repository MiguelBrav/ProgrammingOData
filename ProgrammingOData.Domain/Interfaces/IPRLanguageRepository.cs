using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.Domain.Interfaces;

public interface IPRLanguageRepository
{
    Task<List<PrLanguage>> GetAll();
    Task<PrLanguage> GetById(int id);
    Task<int> Create(PrLanguage language);
    Task Update(PrLanguage language);
    Task Delete(int id);
}
