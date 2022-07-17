using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TaggTimeline.Service.PipelineBehaviours;

namespace TaggTimeline.Service;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection sc)
    {
        sc.AddMediatR(Assembly.GetExecutingAssembly());
        sc.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        sc.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        return sc;
    }
}