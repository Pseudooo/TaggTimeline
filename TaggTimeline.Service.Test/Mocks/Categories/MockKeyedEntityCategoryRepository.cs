
using Moq;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;

namespace TaggTimeline.Service.Test.Mocks.Categories;

public class MockKeyedEntityCategoryRepository : Mock<IKeyedEntityRepository<Category>>
{

    public List<Category> Categories { get; set; }

    public MockKeyedEntityCategoryRepository()
    {
        Categories = CategoryTestData.InitCategories.ToList();

        this.Setup(repo => repo.GetAll())
            .ReturnsAsync(Categories);

        this.Setup(repo => repo.GetById(It.IsAny<Guid>()))
            .ReturnsAsync((Guid id) => Categories.SingleOrDefault(category => category.Id == id));

        this.Setup(repo => repo.SearchForKey(It.IsAny<string>()))
            .ReturnsAsync((string searchTerm) => Categories.Where(category => category.Key.Contains(searchTerm)));

        this.Setup(repo => repo.AddItem(It.IsAny<Category>()))
            .ReturnsAsync((Category added) => {
                added.Id = Guid.NewGuid();
                added.CreatedDate = DateTime.Now;

                Categories.Add(added);

                return added;
            });
    }
}
