
using MapsterMapper;
using Moq;
using NUnit.Framework;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Commands;
using TaggTimeline.Service.Exceptions;
using TaggTimeline.Service.Handlers;
using TaggTimeline.Service.Test.Mocks;
using TaggTimeline.Service.Test.Mocks.Taggs;

namespace TaggTimeline.Service.Test.Commands;

[TestFixture]
public class CreateInstanceCommandTests
{

    public Mock<IKeyedEntityRepository<Tagg>> MockedRepository { get; set; } = null!;
    public Mock<IMapper> MockedMapper { get; set; } = null!;

    [SetUp]
    public void SetUp()
    {
        MockedRepository = new MockKeyedEntityTaggRepository();
        MockedMapper = new MockInstanceMapper();
    }

    [Test]
    public async Task Create_Instance_Should_Create_Instance()
    {
        var command = new CreateInstanceCommand() { TaggId = TaggTestData.InitialTaggs[0].Id };
        var handler = new CreateInstanceHandler(MockedRepository.Object, MockedMapper.Object);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.IsNotNull(result);
        Assert.IsInstanceOf<InstanceModel>(result);
        Assert.AreEqual(TaggTestData.InitialTaggs[0].Instances.Count(), 1);
    }

    [Test]
    public void Create_Instance_Should_Throw_EntityNotFoundException()
    {
        var command = new CreateInstanceCommand() { TaggId = Guid.NewGuid() };
        var handler = new CreateInstanceHandler(MockedRepository.Object, MockedMapper.Object);
        
        Assert.ThrowsAsync<EntityNotFoundException>(async () => {
            await handler.Handle(command, CancellationToken.None);
        });
    }

}
