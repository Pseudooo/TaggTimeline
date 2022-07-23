
using Mapster;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;

namespace TaggTimeline.Service.Configuration;

public class MappingConfig : TypeAdapterConfig
{
    public MappingConfig()
    {
        NewConfig<Tagg, TaggModel>();
        NewConfig<Tagg, TaggPreviewModel>();
        NewConfig<Category, CategoryModel>();
        NewConfig<Category, CategoryPreviewModel>();
        NewConfig<Instance, InstanceModel>();
    }
}
