using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.Domain.Interfaces;

public interface ISupportedLocaleRepository
{
    Task<List<SupportedLocale>> GetAll();
}
