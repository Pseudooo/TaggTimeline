
namespace TaggTimeline.MigrationRunner.Configuration;

public class DatabaseConfiguration
{
    public string ConnectionString { get; set; } = null!;
    public bool DatabaseMigrationsEnabled { get; set; } = true;
}
