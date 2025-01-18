using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.Domain.Interfaces;

public interface IPRLanguageRepository
{
    Task<List<PrLanguage>> GetAll();
    Task<List<PrLanguage>> GetAllByLocale(string locale);
    Task<PrLanguage> GetById(int id);
    Task<PrLanguage> GetByIdAndLocale(int id, string locale);
    Task<int> Create(PrLanguage language);
    Task Update(PrLanguage language);
    Task Delete(int id);
}
