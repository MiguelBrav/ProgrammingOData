using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.Domain.Interfaces;

public interface IPRLanguageRepository
{
    Task<List<PrLanguage>> GetAll();
}
