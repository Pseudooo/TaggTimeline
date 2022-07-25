
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using TaggTimeline.Domain.Context;

namespace TaggTimeline.WebApi.Test;

public class SandboxApplication : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(sc => {
            var contextDescriptor = sc.Single(desc => desc.ServiceType == typeof(DataContext));
            sc.Remove(contextDescriptor);

            var contextOptionsDescriptor = sc.Single(desc => desc.ServiceType == typeof(DbContextOptions<DataContext>));
            sc.Remove(contextOptionsDescriptor);

            sc.AddDbContext<DataContext>(opts => 
            {
                opts.UseNpgsql("User ID=taggserver;Password=Q6%5nWgeN4#9;Host=localhost;Port=5432;Database=taggtimelinesandbox;Pooling=true;Connection Lifetime=0;");
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
