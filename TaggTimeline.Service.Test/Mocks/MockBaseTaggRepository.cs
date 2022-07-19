
using System.Linq.Expressions;
using Moq;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;

namespace TaggTimeline.Service.Test.Mocks;

public static class MockBaseTaggRepository
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

    public static Mock<IBaseRepository<Tagg>> GetBaseRepository()
    {
        var mockRepo = new Mock<IBaseRepository<Tagg>>();
        var innerTags = InitialTaggs.ToList();
        
        mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(innerTags);
        
        foreach(var tagg in InitialTaggs)
        {
            mockRepo.Setup(repo => repo.GetByIdWithNavigationProperties(It.Is<Guid>(id => tagg.Id == id), It.IsAny<Expression<Func<Tagg, object>>>()))
                    .ReturnsAsync(tagg);
        }

        mockRepo.Setup(repo => repo.AddItem(It.IsAny<Tagg>())).ReturnsAsync((Tagg added) => {
            innerTags.Add(added);
            return added;
        });

        return mockRepo;
    }

}
