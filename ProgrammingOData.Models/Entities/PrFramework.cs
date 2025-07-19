namespace ProgrammingOData.Models.Entities;

public class PrFramework
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int LanguageId { get; set; }
    public int CreatedYear { get; set; }
    public string Creator { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public PrFramework() { }

    public PrFramework(string name, int languageId, int createdYear, string creator, string description = "")
    {
        Name = name;
        LanguageId = languageId;
        CreatedYear = createdYear;
        Creator = creator;
        Description = description;
    }
}
