
using System.Linq.Expressions;
using Moq;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;

namespace TaggTimeline.Service.Test.Mocks.Taggs;

public class MockKeyedEntityTaggRepository : Mock<IKeyedEntityRepository<Tagg>>
{

    public List<Tagg> Taggs { get; set; }

    public MockKeyedEntityTaggRepository()
    {
        Taggs = TaggTestData.InitialTaggs.ToList();

        this.Setup(repo => repo.GetAll())
            .ReturnsAsync(Taggs);

        this.Setup(repo => repo.GetByIdWithNavigationProperties(It.IsAny<Guid>(), It.IsAny<Expression<Func<Tagg, object>>[]>()))
            .ReturnsAsync((Guid id, Expression<Func<Tagg, object>>[] _) => Taggs.SingleOrDefault(tagg => tagg.Id == id));

        this.Setup(repo => repo.SearchForKey(It.IsAny<string>()))
            .ReturnsAsync((string searchTerm) => Taggs.Where(tagg => tagg.Key.Contains(searchTerm)));

        this.Setup(repo => repo.AddItem(It.IsAny<Tagg>()))
            .ReturnsAsync((Tagg added) => {
                added.Id = Guid.NewGuid();
                added.CreatedDate = DateTime.Now;

                Taggs.Add(added);

                return added;
            });
    }

    public static Mock<IKeyedEntityRepository<Tagg>> GetBaseRepository()
    {
        var mockRepo = new Mock<IKeyedEntityRepository<Tagg>>();
        var innerTaggs = TaggTestData.InitialTaggs.ToList();
        
        mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(innerTaggs);
        
        foreach(var tagg in innerTaggs)
        {
            mockRepo.Setup(repo => repo.GetByIdWithNavigationProperties(It.Is<Guid>(id => tagg.Id == id), It.IsAny<Expression<Func<Tagg, object>>[]>()))
                    .ReturnsAsync(tagg);
        }
        mockRepo.Setup(repo => repo.SearchForKey(It.IsAny<string>()))
                .ReturnsAsync((string searchTerm) => {
                    return innerTaggs.Where(tagg => tagg.Key.Contains(searchTerm));
                });

        mockRepo.Setup(repo => repo.AddItem(It.IsAny<Tagg>()))
                .ReturnsAsync((Tagg added) => {

                    added.Id = Guid.NewGuid();
                    added.CreatedDate = DateTime.Now;

                    innerTaggs.Add(added);
                    return added;
                });

        return mockRepo;
    }

}
