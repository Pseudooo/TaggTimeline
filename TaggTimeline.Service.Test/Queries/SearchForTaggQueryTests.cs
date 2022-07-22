
using MapsterMapper;
using Moq;
using NUnit.Framework;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Handlers;
using TaggTimeline.Service.Queries;
using TaggTimeline.Service.Test.Mocks;
using TaggTimeline.Service.Test.Mocks.Taggs;

namespace TaggTimeline.Service.Test.Queries;

[TestFixture]
public class SearchForTaggQueryTests
{

    public Mock<IKeyedEntityRepository<Tagg>> MockedRepository { get; private set; } = null!;
    public Mock<IMapper> MockedMapper { get; private set; } = null!;

    [SetUp]
    public void SetUp()
    {
        MockedRepository = new MockKeyedEntityTaggRepository();
        MockedMapper = new MockTaggMapper();
    }

    [Test]
    public async Task Search_For_Taggs_Should_Return_Taggs()
    {
        var searchTerm = "F";
        var query = new SearchForTaggQuery() { SearchTerm = searchTerm };
        var handler = new SearchForTaggHandler(MockedRepository.Object, MockedMapper.Object);
        var result = await handler.Handle(query, CancellationToken.None);

        Assert.IsNotNull(result);
        Assert.AreEqual(result.Count(), 1);
        Assert.IsInstanceOf<IEnumerable<TaggPreviewModel>>(result);
    }

    [Test]
    public async Task Search_For_Taggs_Should_Return_None()
    {
        var searchTerm = "astringthatwontcomeup";
        var query = new SearchForTaggQuery() { SearchTerm = searchTerm };
        var handler = new SearchForTaggHandler(MockedRepository.Object, MockedMapper.Object);
        var result = await handler.Handle(query, CancellationToken.None);

        Assert.IsNotNull(result);
        Assert.AreEqual(result.Count(), 0);
        Assert.IsInstanceOf<IEnumerable<TaggPreviewModel>>(result);
    }

}
