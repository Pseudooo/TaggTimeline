
using MapsterMapper;
using Moq;
using NUnit.Framework;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Commands;
using TaggTimeline.Service.Exceptions;
using TaggTimeline.Service.Handlers;
using TaggTimeline.Service.Test.Mocks.Categories;
using TaggTimeline.Service.Test.Mocks.Taggs;

namespace TaggTimeline.Service.Test.Commands;

[TestFixture]
public class AddCategoryToTaggCommandTests
{
    public Mock<IKeyedEntityRepository<Tagg>> MockedTaggRepository { get; set; } = null!;
    public Mock<IKeyedEntityRepository<Category>> MockedCategoryRepository { get; set; } = null!;
    public Mock<IMapper> MockedMapper { get; set; } = null!;

    [SetUp]
    public void SetUp()
    {
        MockedTaggRepository = new MockKeyedEntityTaggRepository();
        MockedCategoryRepository = new MockKeyedEntityCategoryRepository();
        MockedMapper = new MockTaggMapper();
    }

    [Test]
    public async Task Add_Category_To_Tagg_Should_Categorise_Tagg()
    {
        var command = new AddCategoryToTaggCommand()
        {
            TaggId = TaggTestData.InitialTaggs[1].Id,
            CategoryId = CategoryTestData.InitCategories[0].Id,
        };
        var handler = new AddCategoryToTaggHandler(MockedTaggRepository.Object, 
                                                   MockedCategoryRepository.Object,
                                                   MockedMapper.Object);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.IsNotNull(result);
        Assert.IsInstanceOf<TaggModel>(result);
        Assert.AreEqual(1, result.Categories.Count());
        Assert.AreEqual(command.CategoryId, result.Categories.First().Id);
    }

    [Test]
    public void Add_Invalid_Category_To_Tagg_Should_Throw_EntityNotFoundException()
    {
        var command = new AddCategoryToTaggCommand()
        {
            TaggId = TaggTestData.InitialTaggs[0].Id,
            CategoryId = Guid.NewGuid(),
        };
        var handler = new AddCategoryToTaggHandler(MockedTaggRepository.Object, 
                                                   MockedCategoryRepository.Object,
                                                   MockedMapper.Object);

        Assert.ThrowsAsync<EntityNotFoundException>(async () => {
            await handler.Handle(command, CancellationToken.None);
        });
    }

    [Test]
    public void Add_Category_To_Invalid_Tagg_Should_Throw_EntityNotFoundException()
    {
        var command = new AddCategoryToTaggCommand()
        {
            TaggId = Guid.NewGuid(),
            CategoryId = CategoryTestData.InitCategories[0].Id,
        };
        var handler = new AddCategoryToTaggHandler(MockedTaggRepository.Object, 
                                                   MockedCategoryRepository.Object,
                                                   MockedMapper.Object);

        Assert.ThrowsAsync<EntityNotFoundException>(async () => {
            await handler.Handle(command, CancellationToken.None);
        });
    }

}
