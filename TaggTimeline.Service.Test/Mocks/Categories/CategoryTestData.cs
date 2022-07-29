
using TaggTimeline.Domain.Entities.Taggs;

namespace TaggTimeline.Service.Test.Mocks.Categories;

public static class CategoryTestData
{
    public static List<Category> InitCategories { get; private set; } = new List<Category>()
    {
        new Category()
        {
            Id = Guid.NewGuid(),
            Key = "Important",
            CreatedDate = DateTime.Now,
            ModifiedDate = null,
            DeletedDate = null,
            Taggs = Enumerable.Empty<Tagg>(),
            UserId = "testuserid"
        },
        new Category()
        {
            Id = Guid.NewGuid(),
            Key = "Unimportant",
            CreatedDate = DateTime.Now,
            ModifiedDate = null,
            DeletedDate = null,
            Taggs = Enumerable.Empty<Tagg>(),
            UserId = "testuserid"
        },
    };
}
