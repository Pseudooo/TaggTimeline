
using System.Linq.Expressions;
using Moq;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;

namespace TaggTimeline.Service.Test.Mocks;

public static class MockTaggRepository
{

    public static List<Tagg> InitialTaggs { get; private set; } = new List<Tagg>()
    {
        new Tagg()
            {
                Id = Guid.NewGuid(),
                Key = "TEST TAGG 1",
                CreatedDate = DateTime.Now,
                ModifiedDate = null,
                DeletedDate = null,
                Instances = Enumerable.Empty<Instance>()
            },
        new Tagg()
            {
                Id = Guid.NewGuid(),
                Key = "TEST TAGG 2",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                Instances = Enumerable.Empty<Instance>(),
            },
        new Tagg()
            {
                Id = Guid.NewGuid(),
                Key = "TEST TAGG 3",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                Instances = Enumerable.Empty<Instance>(),
            },
    };

    public static Mock<IBaseRepository<Tagg>> GetBaseRepository()
    {
        var mockRepo = new Mock<IBaseRepository<Tagg>>();

        mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(InitialTaggs);
        
        foreach(var tagg in InitialTaggs)
        {
            mockRepo.Setup(repo => repo.GetByIdWithNavigationProperties(It.Is<Guid>(id => tagg.Id == id), It.IsAny<Expression<Func<Tagg, object>>>()))
                    .ReturnsAsync(tagg);
        }

        return mockRepo;
    }

}
