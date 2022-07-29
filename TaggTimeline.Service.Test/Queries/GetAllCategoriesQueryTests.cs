
using MapsterMapper;
using Moq;
using NUnit.Framework;
using TaggTime.Service.Handlers;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Queries;
using TaggTimeline.Service.Test.Mocks.Categories;

namespace TaggTimeline.Service.Test.Queries;

[TestFixture]
public class GetAllCategoriesQueryTests
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
    public async Task Get_All_Categories_Should_Return_Categories()
    {
        var handler = new GetAllCategoriesHandler(MockedRepository.Object, MockedMapper.Object);
        var result = await handler.Handle(new GetAllCategoriesQuery(), CancellationToken.None);

        Assert.IsNotNull(result);
        Assert.IsInstanceOf<IEnumerable<CategoryPreviewModel>>(result);
        Assert.AreEqual(2, result.Count());
    }

}
