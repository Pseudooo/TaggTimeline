
using MapsterMapper;
using Moq;
using NUnit.Framework;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Handlers;
using TaggTimeline.Service.Queries;
using TaggTimeline.Service.Test.Mocks;

namespace TaggTimeline.Service.Test.Queries;

[TestFixture]
public class GetAllTaggsQueryTests
{

    public Mock<IKeyedEntityRepository<Tagg>> MockedRepository { get; set; } = null!;
    public Mock<IMapper> MockedMapper { get; set; } = null!;
    
    [SetUp]
    public void SetUp()
    {
        this.MockedRepository = MockKeyedEntityTaggRepository.GetBaseRepository();
        this.MockedMapper = MockMapper.GetMapper();
    }

    [Test]
    public async Task Get_All_Taggs_Should_Return_Taggs()
    {
        var handler = new GetAllTaggsHandler(MockedRepository.Object, MockedMapper.Object);
        var result = await handler.Handle(new GetAllTaggsQuery(), CancellationToken.None);

        Assert.IsNotNull(result);
        Assert.AreEqual(result.Count(), 3);
        Assert.IsInstanceOf<IEnumerable<TaggPreviewModel>>(result);
    }

}
