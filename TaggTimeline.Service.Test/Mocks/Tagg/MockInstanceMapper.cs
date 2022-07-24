
using MapsterMapper;
using Moq;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;

namespace TaggTimeline.Service.Test.Mocks.Taggs;

public class MockInstanceMapper : Mock<IMapper>
{
    public MockInstanceMapper()
    {
        Setup(mapper => mapper.Map<InstanceModel>(It.IsAny<Instance>()))
            .Returns((Instance mappedFrom) => {
                return new InstanceModel()
                {
                    Id = mappedFrom.Id,
                    CreatedDate = mappedFrom.CreatedDate,
                    OccuranceDate = mappedFrom.OccuranceDate,
                };
            });

        Setup(mapper => mapper.Map<IEnumerable<InstanceModel>>(It.IsAny<IEnumerable<Instance>>()))
            .Returns((IEnumerable<Instance> mappedFrom) => mappedFrom?.Select(category => this.Object.Map<InstanceModel>(category)).ToList() ?? Enumerable.Empty<InstanceModel>());
    }
}
