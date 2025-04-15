using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.Domain.Interfaces;

public interface IPRLanguageDescriptionRepository
{
    Task<int> CountByLanguage(int languageId);
    Task<int> CountByLanguageLocale(int languageId, string locale);
    Task<List<PrLanguageDescription>> GetAll();
    Task<PrLanguageDescription> GetById(int id);
    Task<int> Create(PrLanguageDescription languageDescription);
    Task Update(PrLanguageDescription languageDescription);
    Task Delete(int id);
}
