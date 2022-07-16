using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace TaggTimeline.Service;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection sc)
    {
        sc.AddMediatR(Assembly.GetExecutingAssembly());

        return sc;
    }
}