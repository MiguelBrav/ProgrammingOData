namespace ProgrammingOData.Models.Entities;

public class PrFrameworkDescription
{
    public int Id { get; set; }
    public int FrameworkId { get; set; }
    public string Locale { get; set; }
    public string Description { get; set; }

    public PrFrameworkDescription() { }
    public PrFrameworkDescription(int frameworkId, string locale, string description)
    {
        FrameworkId = frameworkId;
        Locale = locale;
        Description = description;
    }
}
