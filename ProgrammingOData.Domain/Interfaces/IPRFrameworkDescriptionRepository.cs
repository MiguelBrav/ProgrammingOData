using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.Domain.Interfaces;

public interface IPRFrameworkDescriptionRepository
{
    Task<int> CountByLanguage(int languageId);
    Task<int> CountByLanguageLocale(int languageId, string locale);
}
