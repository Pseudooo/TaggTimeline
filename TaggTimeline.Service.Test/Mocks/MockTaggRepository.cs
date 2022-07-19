
using Moq;
using TaggTimeline.Domain.Interface;

namespace TaggTimeline.Service.Test.Mocks;

public static class MockTaggRepository
{

    public static Mock<ITaggRepository> GetTaggRepository()
    {
        var mockRepo = new Mock<ITaggRepository>();

        mockRepo.Setup(repo => repo.SearchForTagg(It.Is<string>(s => s == "F")))
                .ReturnsAsync(MockBaseTaggRepository.InitialTaggs.Where(tagg => tagg.Key.Contains("F")));
    
        return mockRepo;
    }

}
