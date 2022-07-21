
using Moq;
using NUnit.Framework;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Commands;
using TaggTimeline.Service.Exceptions;
using TaggTimeline.Service.Handlers;
using TaggTimeline.Service.Test.Mocks;

namespace TaggTimeline.Service.Test.Commands;

[TestFixture]
public class CreateInstanceCommandTests
{

    public Mock<IKeyedEntityRepository<Tagg>> MockedRepository { get; set; } = null!;
    public Mock<ITransactionWrapper> MockedTransaction { get; set; } = null!;

    [SetUp]
    public void SetUp()
    {
        MockedRepository = MockKeyedEntityTaggRepository.GetBaseRepository();
        MockedTransaction = MockTransactionWrapper.GetTransaction();
    }

    [Test]
    public async Task Create_Instance_Should_Create_Instance()
    {
        var command = new CreateInstanceCommand() { TaggId = MockKeyedEntityTaggRepository.InitialTaggs[0].Id };
        var handler = new CreateInstanceHandler(MockedRepository.Object, MockedTransaction.Object);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.IsNotNull(result);
        Assert.IsInstanceOf<Instance>(result);
        Assert.AreEqual(MockKeyedEntityTaggRepository.InitialTaggs[0].Instances.Count(), 1);
    }

    [Test]
    public void Create_Instance_Should_Throw_EntityNotFoundException()
    {
        var command = new CreateInstanceCommand() { TaggId = Guid.NewGuid() };
        var handler = new CreateInstanceHandler(MockedRepository.Object, MockedTransaction.Object);
        
        Assert.ThrowsAsync<EntityNotFoundException>(async () => {
            await handler.Handle(command, CancellationToken.None);
        });
    }

}
