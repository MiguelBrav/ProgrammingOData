namespace ProgrammingOData.Models.Entities;

public class PrLanguageDescription
{
    public int Id {  get; set; }    

    public int LanguageId { get; set; }

    public string Locale { get; set; } = string.Empty;

    public string Description {  get; set; } = string.Empty;

    public PrLanguageDescription () { }
    public PrLanguageDescription(int languageId, string locale, string description)
    {
        LanguageId = languageId;
        Locale = locale;
        Description = description;
    }
}
