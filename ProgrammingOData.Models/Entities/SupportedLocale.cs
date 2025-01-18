namespace ProgrammingOData.Models.Entities;

public class SupportedLocale
{
    public int Id { get; set; }
    public string Locale { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
