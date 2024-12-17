namespace ProgrammingOData.Models.Entities;

public class PrLanguage
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int YearCreated { get; set; }

    public string Creator { get; set; } = string.Empty;

    public PrLanguage() { }

    public PrLanguage(string name, int yearCreated, string creator)
    {
        Name = name;
        YearCreated = yearCreated;
        Creator = creator;
    }
}
