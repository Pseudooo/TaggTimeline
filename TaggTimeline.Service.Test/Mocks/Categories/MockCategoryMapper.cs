
using MapsterMapper;
using Moq;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;

namespace TaggTimeline.Service.Test.Mocks.Categories;

public class MockCategoryMapper : Mock<IMapper>
{
    public MockCategoryMapper()
    {
        Setup(mapper => mapper.Map<CategoryModel>(It.IsAny<Category>()))
            .Returns((Category mappedFrom) => {
                return new CategoryModel()
                {
                    Id = mappedFrom.Id,
                    Key = mappedFrom.Key,
                    CreatedDate = mappedFrom.CreatedDate,
                    ModifiedDate = mappedFrom.ModifiedDate,
                    DeletedDate = mappedFrom.DeletedDate,
                };
            });

        Setup(mapper => mapper.Map<IEnumerable<CategoryModel>>(It.IsAny<IEnumerable<Category>>()))
            .Returns((IEnumerable<Category> mappedFrom) => mappedFrom.Select(category => this.Object.Map<CategoryModel>(category)).ToList());
            
        Setup(mapper => mapper.Map<CategoryPreviewModel>(It.IsAny<Category>()))
            .Returns((Category mappedFrom) => {
                return new CategoryPreviewModel()
                {
                    Id = mappedFrom.Id,
                    Key = mappedFrom.Key,
                };
            });

        Setup(mapper => mapper.Map<IEnumerable<CategoryPreviewModel>>(It.IsAny<IEnumerable<Category>>()))
            .Returns((IEnumerable<Category> mappedFrom) => mappedFrom.Select(category => this.Object.Map<CategoryPreviewModel>(category)).ToList());
    }
}
