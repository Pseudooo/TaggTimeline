
using Moq;
using NUnit.Framework;
using TaggTime.Service.Queries;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Handlers;
using TaggTimeline.Service.Test.Mocks.Categories;

namespace TaggTimeline.Service.Test.Queries;

[TestFixture]
public class SearchForCategoryQueryTests
{

    public Mock<IKeyedEntityRepository<Category>> MockedRepository { get; set; } = null!;

    [SetUp]
    public void SetUp()
    {
        MockedRepository = new MockKeyedEntityCategoryRepository();
    }

    [Test]
    public async Task Search_For_Categories_Should_Return_Categories()
    {
        var searchTerm = "ant";
        var query = new SearchForCategoriesQuery() { SearchTerm = searchTerm };
        var handler = new SearchForCategoriesHandler(MockedRepository.Object);
        var result = await handler.Handle(query, CancellationToken.None);
        
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<IEnumerable<CategoryPreviewModel>>(result);
        Assert.AreEqual(result.Count(), 2);
    }

    [Test]
    public async Task Search_For_Categories_Should_Return_None()
    {
        var searchTerm = "astringthatwontcomeup";
        var query = new SearchForCategoriesQuery() { SearchTerm = searchTerm };
        var handler = new SearchForCategoriesHandler(MockedRepository.Object);
        var result = await handler.Handle(query, CancellationToken.None);

        Assert.IsNotNull(result);
        Assert.IsInstanceOf<IEnumerable<CategoryPreviewModel>>(result);
        Assert.AreEqual(result.Count(), 0);
    }

}
