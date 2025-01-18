namespace ProgrammingOData.Models.Entities;

public class PrLanguageDescription
{
    public int Id {  get; set; }    

    public int LanguageId { get; set; }

    public string Locale { get; set; } = string.Empty;

    public string Description {  get; set; } = string.Empty;    
}
