
using Moq;
using NUnit.Framework;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Commands;
using TaggTimeline.Service.Handlers;
using TaggTimeline.Service.Test.Mocks;

namespace TaggTimeline.Service.Test.Commands;

[TestFixture]
public class CreateTaggCommandTests
{

    public Mock<IBaseRepository<Tagg>> MockedRepository { get; set; } = null!;

    [SetUp]
    public void SetUp()
    {
        MockedRepository = MockBaseTaggRepository.GetBaseRepository();
    }

    [Test]
    public async Task Create_Tagg_Should_Create_Tagg()
    {
        var command = new CreateTaggCommand() { Key = "Tagg" };
        var handler = new CreateTaggHandler(MockedRepository.Object);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.IsNotNull(result);
        Assert.IsInstanceOf<Tagg>(result);
        Assert.AreEqual((await MockedRepository.Object.GetAll()).Count(), 4);
    }

}
