
using Moq;
using NUnit.Framework;
using TaggTime.Service.Handlers;
using TaggTime.Service.Queries;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Test.Mocks;

namespace TaggTimeline.Service.Test.Queries;

[TestFixture]
public class GetAllCategoriesQueryTests
{
    
    public Mock<IKeyedEntityRepository<Category>> MockedRepository { get; set; } = null!;

    [SetUp]
    public void SetUp()
    {
        MockedRepository = MockKeyedEntityCategoryRepository.GetBaseRepository();
    }

    [Test]
    public async Task Get_All_Categories_Should_Return_Taggs()
    {
        var handler = new GetAllCategoriesHandler(MockedRepository.Object);
        var result = await handler.Handle(new GetAllCategoriesQuery(), CancellationToken.None);

        Assert.IsNotNull(result);
        Assert.IsInstanceOf<IEnumerable<Category>>(result);
        Assert.AreEqual(result.Count(), 2);
    }

}
