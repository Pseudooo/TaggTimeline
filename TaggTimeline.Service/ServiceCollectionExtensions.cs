using System.Reflection;
using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TaggTimeline.Service.Configuration;
using TaggTimeline.Service.PipelineBehaviours;

namespace TaggTimeline.Service;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection sc)
    {
        sc.AddMediatR(Assembly.GetExecutingAssembly());
        sc.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        sc.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        sc.AddSingleton(new MappingConfig());
        sc.AddScoped<IMapper, ServiceMapper>();

        return sc;
    }
}