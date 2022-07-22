
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;

namespace TaggTimeline.Service.Test.Mocks.Taggs;

public static class TaggTestData
{

    public static List<Tagg> InitialTaggs { get; private set; } = new List<Tagg>()
    {
        new Tagg()
            {
                Id = Guid.NewGuid(),
                Key = "FOO",
                CreatedDate = DateTime.Now,
                ModifiedDate = null,
                DeletedDate = null,
                Instances = Enumerable.Empty<Instance>()
            },
        new Tagg()
            {
                Id = Guid.NewGuid(),
                Key = "BAR",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                Instances = Enumerable.Empty<Instance>(),
            },
        new Tagg()
            {
                Id = Guid.NewGuid(),
                Key = "KEK",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                Instances = Enumerable.Empty<Instance>(),
            },
    };

    public static IList<TaggModel> InitialTaggModels { get; private set; } = new List<TaggModel>()
    {
        new TaggModel()
            {
                Id = InitialTaggs[0].Id,
                Key = InitialTaggs[0].Key,
                CreatedDate = DateTime.Now,
                ModifiedDate = null,
                DeletedDate = null,
                Instances = Enumerable.Empty<InstanceModel>(),
            },
        new TaggModel()
            {
                Id = InitialTaggs[1].Id,
                Key = InitialTaggs[1].Key,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                Instances = Enumerable.Empty<InstanceModel>(),
            },
        new TaggModel()
            {
                Id = InitialTaggs[2].Id,
                Key = InitialTaggs[2].Key,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                Instances = Enumerable.Empty<InstanceModel>(),
            },
    };

    public static IList<TaggPreviewModel> InitialTaggPreviewModels { get; private set; } = new List<TaggPreviewModel>()
    {
        new TaggPreviewModel()
        {
            Id = InitialTaggModels[0].Id,
            Key = InitialTaggs[0].Key
        },
        new TaggPreviewModel()
        {
            Id = InitialTaggModels[1].Id,
            Key = InitialTaggs[1].Key
        },
        new TaggPreviewModel()
        {
            Id = InitialTaggModels[2].Id,
            Key = InitialTaggs[2].Key
        }
    };

}
