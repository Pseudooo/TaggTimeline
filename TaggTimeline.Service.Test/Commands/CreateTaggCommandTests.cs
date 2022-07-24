
using MapsterMapper;
using Moq;
using NUnit.Framework;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Commands;
using TaggTimeline.Service.Handlers;
using TaggTimeline.Service.Test.Mocks.Taggs;

namespace TaggTimeline.Service.Test.Commands;

[TestFixture]
public class CreateTaggCommandTests
{
    public Mock<IKeyedEntityRepository<Tagg>> MockedRepository { get; set; } = null!;

    public Mock<IMapper> MockedMapper { get; set; } = null!;

    [SetUp]
    public void SetUp()
    {
        MockedRepository = new MockKeyedEntityTaggRepository();
        MockedMapper = new MockTaggMapper();
    }

    [Test]
    public async Task Create_Tagg_Should_Create_Tagg()
    {
        var command = new CreateTaggCommand() { Key = "Tagg" };
        var handler = new CreateTaggHandler(MockedRepository.Object, MockedMapper.Object);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.IsNotNull(result);
        Assert.IsInstanceOf<TaggModel>(result);
        Assert.AreEqual((await MockedRepository.Object.GetAll()).Count(), 4);
    }

}
