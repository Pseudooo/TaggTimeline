
using Moq;
using NUnit.Framework;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Handlers;
using TaggTimeline.Service.Queries;
using TaggTimeline.Service.Test.Mocks;

namespace TaggTimeline.Service.Test.Handlers;

[TestFixture]
public class GetAllTaggsHandlersTests
{

    public Mock<IBaseRepository<Tagg>> MockedRepository { get; set; }
    
    [SetUp]
    public void SetUp()
    {
        this.MockedRepository = MockTaggRepository.GetBaseRepository();
    }

    [Test]
    public async Task Get_All_Taggs_Should_Return_Taggs()
    {
        var handler = new GetAllTagsHandler(MockedRepository.Object);
        var result = await handler.Handle(new GetAllTagsQuery(), CancellationToken.None);

        Assert.IsNotNull(result);
        Assert.AreEqual(result.Count(), 3);
    }

}
