
namespace TaggTimeline.MigrationRunner.Configuration;

public class DatabaseConfiguration
{
    public string ConnectionString { get; set; }
    
    public bool DatabaseMigrationsEnabled { get; set; } = true;
    public bool ForceRebuild { get; set; }

}
