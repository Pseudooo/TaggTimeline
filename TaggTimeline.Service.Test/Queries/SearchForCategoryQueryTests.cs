
using MapsterMapper;
using Moq;
using NUnit.Framework;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Handlers;
using TaggTimeline.Service.Queries;
using TaggTimeline.Service.Test.Mocks.Categories;

namespace TaggTimeline.Service.Test.Queries;

[TestFixture]
public class SearchForCategoryQueryTests
{

    public Mock<IKeyedEntityRepository<Category>> MockedRepository { get; set; } = null!;
    public Mock<IMapper> MockedMapper { get; set; } = null!;

    [SetUp]
    public void SetUp()
    {
        MockedRepository = new MockKeyedEntityCategoryRepository();
        MockedMapper = new MockCategoryMapper();
    }

    [Test]
    public async Task Search_For_Categories_Should_Return_Categories()
    {
        var searchTerm = "ant";
        var query = new SearchForCategoriesQuery() { SearchTerm = searchTerm, UserId = "testuserid" };
        var handler = new SearchForCategoriesHandler(MockedRepository.Object, MockedMapper.Object);
        var result = await handler.Handle(query, CancellationToken.None);
        
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<IEnumerable<CategoryPreviewModel>>(result);
        Assert.AreEqual(2, result.Count());
    }

    [Test]
    public async Task Search_For_Categories_Should_Return_None()
    {
        var searchTerm = "astringthatwontcomeup";
        var query = new SearchForCategoriesQuery() { SearchTerm = searchTerm, UserId = "testuserid" };
        var handler = new SearchForCategoriesHandler(MockedRepository.Object, MockedMapper.Object);
        var result = await handler.Handle(query, CancellationToken.None);

        Assert.IsNotNull(result);
        Assert.IsInstanceOf<IEnumerable<CategoryPreviewModel>>(result);
        Assert.AreEqual(result.Count(), 0);
    }

}
