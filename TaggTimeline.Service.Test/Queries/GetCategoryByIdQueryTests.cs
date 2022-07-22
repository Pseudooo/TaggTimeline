
using MapsterMapper;
using Moq;
using NUnit.Framework;
using TaggTime.Service.Handlers;
using TaggTime.Service.Queries;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Exceptions;
using TaggTimeline.Service.Test.Mocks.Categories;

namespace TaggTimeline.Service.Test.Queries;

[TestFixture]
public class GetCategoryByIdQueryTests
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
    public async Task Get_Category_By_Id_Should_Return_Category()
    {
        var id = CategoryTestData.InitCategories[0].Id;
        var query = new GetCategoryByIdQuery() { Id = id };
        var handler = new GetCategoryByIdHandler(MockedRepository.Object, MockedMapper.Object);
        var result = await handler.Handle(query, CancellationToken.None);

        Assert.IsNotNull(result);
        Assert.IsInstanceOf<CategoryModel>(result);
        Assert.IsNotEmpty(result.Key);
    }

    [Test]
    public void Get_Category_By_Id_Should_Throw_EntityNotFoundException()
    {
        var id = Guid.NewGuid();
        var query = new GetCategoryByIdQuery() { Id = id };
        var handler = new GetCategoryByIdHandler(MockedRepository.Object, MockedMapper.Object);
        
        Assert.ThrowsAsync<EntityNotFoundException>(async () => {
            var result = await handler.Handle(query, CancellationToken.None);
        });
    }

}
