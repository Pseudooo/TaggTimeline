
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using TaggTimeline.Domain.Configuration;
using TaggTimeline.Domain.Context;

namespace TaggTimeline.WebApi.Test;

public class SandboxApplication : WebApplicationFactory<Program>
{
    public IServiceProvider ServiceProvider { get; private set; } = null!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(sc => {

            // Get the loaded configuration from application
            DatabaseConfiguration databaseConfiguration;
            using(var scope = sc.BuildServiceProvider().CreateScope())
            {
                var provider = scope.ServiceProvider;
                databaseConfiguration = provider.GetRequiredService<DatabaseConfiguration>();
            }
            
            // Modify the database param to be a sandbox database
            var testDbConnectionStringBuilder = new NpgsqlConnectionStringBuilder(databaseConfiguration.ConnectionString);

            // Remove the original context from the service collection
            var contextDescriptor = sc.Single(desc => desc.ServiceType == typeof(DataContext));
            sc.Remove(contextDescriptor);
            var contextOptionsDescriptor = sc.Single(desc => desc.ServiceType == typeof(DbContextOptions<DataContext>));
            sc.Remove(contextOptionsDescriptor);

            Console.WriteLine($"Using Connection String:\n{testDbConnectionStringBuilder.ConnectionString}");
            // Add new context with modified configuration
            sc.AddDbContext<DataContext>(opts => 
            {
                opts.UseNpgsql(testDbConnectionStringBuilder.ConnectionString);
            });

            using(var scope = sc.BuildServiceProvider().CreateScope())
            {
                var provider = scope.ServiceProvider;
                var context = provider.GetRequiredService<DataContext>();

                context.Database.EnsureCreated();
            }
        });
    }
}
