
namespace TaggTimeline.WebApi.Test.Configuration;

public class TestConfiguration
{
    public bool OverrideWebapiConnectionString { get; set; }
    public string DatabaseConnectionString { get; set; } = null!;
}
