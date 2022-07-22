
using MapsterMapper;
using Moq;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;

namespace TaggTimeline.Service.Test.Mocks;

public static class MockMapper
{

    public static Mock<IMapper> GetMapper()
    {
        var mock = new Mock<IMapper>();
        
        foreach(var tagg in MockKeyedEntityTaggRepository.InitialTaggs)
        {
            mock.Setup(mapper => mapper.Map<TaggModel>(It.Is<Tagg>(t => t == tagg)))
                .Returns((Tagg mappedFrom) => MockKeyedEntityTaggRepository.InitialTaggModels.Single(t => t.Id == mappedFrom.Id));
        }

        foreach(var tagg in MockKeyedEntityTaggRepository.InitialTaggs)
        {
            mock.Setup(mapper => mapper.Map<TaggPreviewModel>(It.Is<Tagg>(t => t == tagg)))
                .Returns((Tagg mappedFrom) => MockKeyedEntityTaggRepository.InitialTaggPreviewModels.Single(t => t.Id == mappedFrom.Id));
        }
        mock.Setup(mapper => mapper.Map<IEnumerable<TaggPreviewModel>>(It.IsAny<IEnumerable<Tagg>>()))
            .Returns((IEnumerable<Tagg> mappedFrom) => {
                return mappedFrom.Select(tagg => mock.Object.Map<TaggPreviewModel>(tagg)).ToList();
            });

        return mock;
    }

}
