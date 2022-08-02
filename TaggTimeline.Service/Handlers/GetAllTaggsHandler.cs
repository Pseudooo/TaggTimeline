
using MapsterMapper;
using MediatR;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Queries;

namespace TaggTimeline.Service.Handlers;

public class GetAllTaggsHandler : IRequestHandler<GetAllTaggsQuery, IEnumerable<TaggPreviewModel>>
{
    private readonly IBaseRepository<Tagg> _baseRepository;
    private readonly IMapper _mapper;

    public GetAllTaggsHandler(IBaseRepository<Tagg> baseRepository, IMapper mapper)
    {
        _baseRepository = baseRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaggPreviewModel>> Handle(GetAllTaggsQuery request, CancellationToken cancellationToken)
    {
        var taggs = await _baseRepository.GetAllFromUser<Tagg>(request.UserId!);

        var taggPreviews = _mapper.Map<IEnumerable<TaggPreviewModel>>(taggs);

        return taggPreviews;
    }
}
