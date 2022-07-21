
using Moq;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;

namespace TaggTimeline.Service.Test.Mocks;

public static class MockKeyedEntityCategoryRepository
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
        },
        new Category()
        {
            Id = Guid.NewGuid(),
            Key = "Unimportant",
            CreatedDate = DateTime.Now,
            ModifiedDate = null,
            DeletedDate = null,
            Taggs = Enumerable.Empty<Tagg>(),
        },
    };

    public static Mock<IKeyedEntityRepository<Category>> GetBaseRepository()
    {
        var mockRepo = new Mock<IKeyedEntityRepository<Category>>();
        var innerCategories = InitCategories.ToList();

        mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(innerCategories);

        foreach(var category in InitCategories)
        {
            mockRepo.Setup(repo => repo.GetById(It.Is<Guid>(id => category.Id == id)))
                    .ReturnsAsync(category);
        }
        
        mockRepo.Setup(repo => repo.SearchForKey(It.IsAny<string>()))
                .ReturnsAsync((string searchTerm) => {
                    return innerCategories.Where(category => category.Key.Contains(searchTerm));
                });

        mockRepo.Setup(repo => repo.AddItem(It.IsAny<Category>()))
                .ReturnsAsync((Category added) => {
                    innerCategories.Add(added);
                    return added;
                });
        
        return mockRepo;
    }

}
