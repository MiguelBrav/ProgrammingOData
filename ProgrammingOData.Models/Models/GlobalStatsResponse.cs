namespace ProgrammingOData.Models.Models;

public class GlobalStatsResponse
{
    public FrameworkStats Frameworks { get; set; } = new();
    public int Languages { get; set; }
    public DescriptionStats Descriptions { get; set; } = new();
}

public class FrameworkStats
{
    public int Total { get; set; }
    public int WithDescriptions { get; set; }
}

public class DescriptionStats
{
    public int Total { get; set; }
    public List<string> Locales { get; set; } = new();
}

public class GlobalStatsRaw
{
    public long TotalFrameworks { get; set; }
    public long FrameworksWithDescriptions { get; set; }
    public long TotalLanguages { get; set; }
    public long TotalDescriptions { get; set; }
    public string? Locales { get; set; }
}


