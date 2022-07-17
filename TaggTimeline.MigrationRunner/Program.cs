using System.Reflection;
using DbUp;
using Microsoft.Extensions.Configuration;
using TaggTimeline.MigrationRunner.Configuration;

internal class Program
{
    private static int Main(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        var configuration = builder.Build().Get<DatabaseConfiguration>();

        // Quit early if migrations disabled
        if(!configuration.DatabaseMigrationsEnabled)
            return 0;

        var upgrader = DeployChanges.To
                                    .PostgresqlDatabase(configuration.ConnectionString)
                                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                                    .LogToConsole()
                                    .Build();

        var result = upgrader.PerformUpgrade();

        if(result.Successful)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Database upgraded");
            Console.ResetColor();
            return 0;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Upgrade failed");
            Console.WriteLine(result.Error);
            Console.ResetColor();
            return -1;
        }

    }
}