using System.Reflection;
using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Service.PipelineBehaviours;

namespace TaggTimeline.Service;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection sc)
    {
        sc.AddMediatR(Assembly.GetExecutingAssembly());
        sc.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        sc.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        var mappingConfig = new TypeAdapterConfig();
        mappingConfig.NewConfig<Tagg, TaggModel>();;
        mappingConfig.NewConfig<Tagg, TaggPreviewModel>();
        mappingConfig.NewConfig<Category, CategoryModel>();
        mappingConfig.NewConfig<Category, CategoryPreviewModel>();
        mappingConfig.NewConfig<Instance, InstanceModel>();

        sc.AddSingleton(mappingConfig);
        sc.AddScoped<IMapper, ServiceMapper>();

        return sc;
    }
}