namespace ProgrammingOData.Domain.Interfaces;

public interface IPRLanguageDescriptionRepository
{
    Task<int> CountByLanguage(int languageId);

}
