
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using TaggTimeline.Domain.Configuration;
using TaggTimeline.Domain.Context;
using TaggTimeline.WebApi.Test.Configuration;

namespace TaggTimeline.WebApi.Test.WebApplicationFactory;

public class SandboxApplication : WebApplicationFactory<Program>
{   
    private readonly TestConfiguration _testConfiguration;

    public DataContext Context { get; private set; } = null!;

    public SandboxApplication(TestConfiguration testConfiguration)
    {
        _testConfiguration = testConfiguration;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(sc => {

            // Get the loaded configuration from application
            DatabaseConfiguration databaseConfiguration;
            using(var scope = sc.BuildServiceProvider().CreateScope())
            {
                var sp = scope.ServiceProvider;
                databaseConfiguration = sp.GetRequiredService<DatabaseConfiguration>();
            }
            
            // Modify the database param to be a sandbox database
            var testDbConnectionStringBuilder = new NpgsqlConnectionStringBuilder(databaseConfiguration.ConnectionString);

            if(_testConfiguration.OverrideWebapiConnectionString)
            {
                Console.WriteLine($"Using Sandbox Application, configuring now");

                // Remove existing context
                var contextDescriptor = sc.Single(descriptor => descriptor.ServiceType == typeof(DataContext));
                var contextOptionsDescriptor = sc.Single(descriptor => descriptor.ServiceType == typeof(DbContextOptions<DataContext>));
                sc.Remove(contextDescriptor);
                sc.Remove(contextOptionsDescriptor);

                // Add new context with new connection string
                var connectionStringBuilder = new NpgsqlConnectionStringBuilder(_testConfiguration.ConnectionString);
                sc.AddDbContext<DataContext>(options => 
                {
                    options.UseNpgsql(connectionStringBuilder.ConnectionString);
                });

            }

            var provider = sc.BuildServiceProvider().CreateScope().ServiceProvider;
            Context = provider.GetRequiredService<DataContext>();
        });
    }
}
