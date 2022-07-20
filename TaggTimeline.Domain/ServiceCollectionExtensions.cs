using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using TaggTimeline.Domain.Configuration;
using TaggTimeline.Domain.Context;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Domain.Repository;

namespace TaggTimeline.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainDependencies(this IServiceCollection sc, DatabaseConfiguration dbConfig)
    {
        var builder = new NpgsqlConnectionStringBuilder(dbConfig.ConnectionString);
        sc.AddDbContext<DataContext>(opts => opts.UseNpgsql(builder.ConnectionString));

        sc.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        sc.AddScoped<ITaggRepository, TaggRepository>();
        sc.AddScoped<ITransaction, Transaction>();

        return sc;
    }
}