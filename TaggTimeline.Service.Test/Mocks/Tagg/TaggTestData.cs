
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Service.Test.Mocks.Categories;

namespace TaggTimeline.Service.Test.Mocks.Taggs;

public static class TaggTestData
{

    public static List<Instance> InitialInstances { get; private set; } = new List<Instance>()
    {
        new Instance()
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                OccuranceDate = DateTime.Now,
            },
    };

    public static List<Tagg> InitialTaggs { get; private set; } = new List<Tagg>()
    {
        new Tagg()
            {
                Id = Guid.NewGuid(),
                Key = "FOO",
                CreatedDate = DateTime.Now,
                ModifiedDate = null,
                DeletedDate = null,
                Instances = InitialInstances,
                Categories = new[] { CategoryTestData.InitCategories[0] },
                UserId = "testuserid"
            },
        new Tagg()
            {
                Id = Guid.NewGuid(),
                Key = "BAR",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                Instances = Enumerable.Empty<Instance>(),
                Categories = Enumerable.Empty<Category>(),
                UserId = "testuserid"
            },
        new Tagg()
            {
                Id = Guid.NewGuid(),
                Key = "KEK",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                Instances = Enumerable.Empty<Instance>(),
                Categories = Enumerable.Empty<Category>(),
                UserId = "testuserid"
            },
    };



}
