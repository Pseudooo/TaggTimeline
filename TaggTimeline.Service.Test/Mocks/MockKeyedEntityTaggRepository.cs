
using System.Linq.Expressions;
using Moq;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;

namespace TaggTimeline.Service.Test.Mocks;

public class MockKeyedEntityTaggRepository : Mock<IKeyedEntityRepository<Tagg>>
{

    private List<Tagg> _taggs;

    public MockKeyedEntityTaggRepository()
    {
        _taggs = InitialTaggs.ToList();

        this.Setup(repo => repo.GetAll())
            .ReturnsAsync(_taggs);

        this.Setup(repo => repo.GetByIdWithNavigationProperties(It.IsAny<Guid>(), It.IsAny<Expression<Func<Tagg, object>>[]>()))
            .ReturnsAsync((Guid id, Expression<Func<Tagg, object>>[] _) => _taggs.SingleOrDefault(tagg => tagg.Id == id));

        this.Setup(repo => repo.SearchForKey(It.IsAny<string>()))
            .ReturnsAsync((string searchTerm) => _taggs.Where(tagg => tagg.Key.Contains(searchTerm)));

        this.Setup(repo => repo.AddItem(It.IsAny<Tagg>()))
            .ReturnsAsync((Tagg added) => {
                added.Id = Guid.NewGuid();
                added.CreatedDate = DateTime.Now;

                _taggs.Add(added);

                return added;
            });
    }

    public static Mock<IKeyedEntityRepository<Tagg>> GetBaseRepository()
    {
        var mockRepo = new Mock<IKeyedEntityRepository<Tagg>>();
        var innerTaggs = InitialTaggs.ToList();
        
        mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(innerTaggs);
        
        foreach(var tagg in InitialTaggs)
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
                Key = "FOO",
                CreatedDate = DateTime.Now,
                ModifiedDate = null,
                DeletedDate = null,
                Instances = Enumerable.Empty<InstanceModel>(),
            },
        new TaggModel()
            {
                Id = InitialTaggs[1].Id,
                Key = "BAR",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                Instances = Enumerable.Empty<InstanceModel>(),
            },
        new TaggModel()
            {
                Id = InitialTaggs[2].Id,
                Key = "KEK",
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
