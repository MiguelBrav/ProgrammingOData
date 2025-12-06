using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.Domain.Interfaces;

public interface IPRFrameworkDescriptionRepository
{
    Task<int> CountByLanguage(int frameworkId);
    Task<int> CountByLanguageLocale(int frameworkId, string locale);
    Task<PrFrameworkDescription> GetById(int id);
    Task<int> Create(PrFrameworkDescription frameworkDescription);
    Task Update(PrFrameworkDescription frameworkDescription);
}
