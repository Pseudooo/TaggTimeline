
using MediatR;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Queries;

namespace TaggTimeline.Service.Handlers;

public class GetAllTagsHandler : IRequestHandler<GetAllTagsQuery, IEnumerable<TaggPreviewModel>>
{
    private readonly IBaseRepository<Tagg> _baseRepository;

    public GetAllTagsHandler(IBaseRepository<Tagg> baseRepository)
    {
        _baseRepository = baseRepository;
    }

    public async Task<IEnumerable<TaggPreviewModel>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var taggs = await _baseRepository.GetAll();

        var taggPreviews = taggs.Select(tagg => new TaggPreviewModel() { Id = tagg.Id, Key = tagg.Key }).ToList();

        return taggPreviews;
    }
}