
using MapsterMapper;
using Moq;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Service.Test.Mocks.Categories;

namespace TaggTimeline.Service.Test.Mocks.Taggs;

public class MockTaggMapper : Mock<IMapper>
{

    public MockTaggMapper()
    {
        var categoryMapper = new MockCategoryMapper().Object;
        var instanceMapper = new MockInstanceMapper().Object;

        Setup(mapper => mapper.Map<TaggModel>(It.IsAny<Tagg>()))
            .Returns((Tagg mappedFrom) => {
                return new TaggModel()
                {
                    Id = mappedFrom.Id,
                    Key = mappedFrom.Key,
                    CreatedDate = mappedFrom.CreatedDate,
                    ModifiedDate = mappedFrom.ModifiedDate,
                    DeletedDate = mappedFrom.DeletedDate,
                    Categories = categoryMapper.Map<IEnumerable<CategoryPreviewModel>>(mappedFrom.Categories),
                    Instances = instanceMapper.Map<IEnumerable<InstanceModel>>(mappedFrom.Instances),
                };
            });
            
        Setup(mapper => mapper.Map<IEnumerable<TaggModel>>(It.IsAny<IEnumerable<Tagg>>()))
            .Returns((IEnumerable<Tagg> mappedFrom) => mappedFrom.Select(tagg => this.Object.Map<TaggModel>(tagg)).ToList());

        Setup(mapper => mapper.Map<TaggPreviewModel>(It.IsAny<Tagg>()))
            .Returns((Tagg mappedFrom) => {
                return new TaggPreviewModel()
                {
                    Id = mappedFrom.Id,
                    Key = mappedFrom.Key,
                };
            });

        Setup(mapper => mapper.Map<IEnumerable<TaggPreviewModel>>(It.IsAny<IEnumerable<Tagg>>()))
            .Returns((IEnumerable<Tagg> mappedFrom) => mappedFrom.Select(tagg => this.Object.Map<TaggPreviewModel>(tagg)).ToList());
    }

}
