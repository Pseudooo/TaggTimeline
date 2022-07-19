
using Moq;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;

namespace TaggTimeline.Service.Test.Mocks;

public static class MockTaggRepository
{

    public static Mock<IBaseRepository<Tagg>> GetBaseRepository()
    {
        var taggs = new List<Tagg>()
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

        var mockRepo = new Mock<IBaseRepository<Tagg>>();

        mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(taggs);

        return mockRepo;

    }

}
