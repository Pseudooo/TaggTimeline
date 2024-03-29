
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
        var command = new CreateTaggCommand() { Key = "Tagg", UserId = "testuserid", Colour = "#CAE9F5" };
        var handler = new CreateTaggHandler(MockedRepository.Object, MockedMapper.Object);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.IsNotNull(result);
        Assert.IsInstanceOf<TaggModel>(result);
        Assert.IsNotNull(result.Instances);
        Assert.IsInstanceOf<IEnumerable<InstanceModel>>(result.Instances);
        Assert.IsNotNull(result.Categories);
        Assert.IsInstanceOf<IEnumerable<CategoryPreviewModel>>(result.Categories);
        Assert.AreEqual((await MockedRepository.Object.GetAllFromUser<Tagg>("testuserid")).Count(), 4);
    }

}
