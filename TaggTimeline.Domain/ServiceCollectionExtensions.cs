using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using TaggTimeline.Domain.Configuration;

namespace TaggTimeline.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainDependencies(this IServiceCollection sc, DatabaseConfiguration dbConfig)
    {
        var builder = new NpgsqlConnectionStringBuilder(dbConfig.ConnectionString);
        sc.AddDbContext<DataContext>(opts => opts.UseNpgsql(builder.ConnectionString));

        return sc;
    }
}