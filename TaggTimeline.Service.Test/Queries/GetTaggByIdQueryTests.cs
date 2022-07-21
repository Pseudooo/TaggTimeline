
using Moq;
using NUnit.Framework;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Exceptions;
using TaggTimeline.Service.Handlers;
using TaggTimeline.Service.Queries;
using TaggTimeline.Service.Test.Mocks;

namespace TaggTimeline.Service.Test.Queries;

[TestFixture]
public class GetTaggByIdQueryTests
{

    public Mock<IKeyedEntityRepository<Tagg>> MockedRepository { get; set; } = null!;

    [SetUp]
    public void SetUp()
    {   
        MockedRepository = MockKeyedEntityTaggRepository.GetBaseRepository();
    }

    [Test]
    public async Task Get_Tagg_By_Id_Should_Return_Tagg()
    {
        var id = MockKeyedEntityTaggRepository.InitialTaggs[0].Id;
        var query = new GetTaggByIdQuery() { Id = id };
        var handler = new GetTaggByIdHandler(MockedRepository.Object);
        var result = await handler.Handle(query, CancellationToken.None);

        Assert.IsNotNull(result);
        Assert.IsNotEmpty(result.Key);
        Assert.IsInstanceOf<Tagg>(result);
    }

    [Test]
    public void Get_Tagg_By_Id_Should_Throw_EntityNotFoundException()
    {
        var id = Guid.NewGuid();
        var query = new GetTaggByIdQuery() { Id = id };
        var handler = new GetTaggByIdHandler(MockedRepository.Object);
        
        Assert.ThrowsAsync<EntityNotFoundException>(async () => {
            var result = await handler.Handle(query, CancellationToken.None);
        });
    }

}
