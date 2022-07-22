
using Moq;
using NUnit.Framework;
using TaggTime.Service.Commands;
using TaggTime.Service.Handlers;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Test.Mocks;
using TaggTimeline.Service.Test.Mocks.Categories;

namespace TaggTimeline.Service.Test.Commands;

[TestFixture]
public class CreateCategoryCommandTests
{

    public Mock<IKeyedEntityRepository<Category>> MockedRepository { get; set; } = null!;

    public Mock<ITransactionWrapper> MockedTransaction { get; set; } = null!;

    [SetUp]
    public void SetUp()
    {
        MockedRepository = new MockKeyedEntityCategoryRepository();
        MockedTransaction = MockTransactionWrapper.GetTransaction();
    }

    [Test]
    public async Task Create_Category_Should_Create_Catgory()
    {
        var command = new CreateCategoryCommand() { Key = "Category" };
        var handler = new CreateCategoryHandler(MockedRepository.Object, MockedTransaction.Object);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.IsNotNull(result);
        Assert.IsInstanceOf<Category>(result);
        Assert.AreEqual((await MockedRepository.Object.GetAll()).Count(), 3);
    }

}
