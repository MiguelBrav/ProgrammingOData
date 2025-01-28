using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.Domain.Interfaces;

public interface IPRLanguageDescriptionRepository
{
    Task<int> CountByLanguage(int languageId);
    Task<List<PrLanguageDescription>> GetAll();
}
