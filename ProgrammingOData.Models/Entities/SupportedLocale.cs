namespace ProgrammingOData.Models.Entities;

public class SupportedLocale
{
    public int Id { get; set; }
    public string Locale { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }

    public SupportedLocale() { }

    public SupportedLocale(string locale, string name)
    {
        Locale = locale;
        Name = name;
        IsActive = true; 
    }

    public SupportedLocale(int id, string locale, string name, bool isActive)
    {
        Id = id;
        Locale = locale;
        Name = name;
        IsActive= isActive;
    }    

}
