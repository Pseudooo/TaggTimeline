using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace TaggTimeline.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainDependencies(this IServiceCollection sc)
    {
        var connectionString = "User ID=taggserver;Password=Q6%5nWgeN4#9;Host=localhost;Port=5432;Database=taggtimeline;Pooling=true;Connection Lifetime=0;";
        var builder = new NpgsqlConnectionStringBuilder(connectionString);

        sc.AddDbContext<DataContext>(opts => opts.UseNpgsql(builder.ConnectionString));

        return sc;
    }
}