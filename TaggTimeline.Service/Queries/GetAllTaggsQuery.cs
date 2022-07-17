
using MediatR;
using TaggTimeline.ClientModel.Taggs;

namespace TaggTimeline.Service.Queries;

public class GetAllTagsQuery : IRequest<IEnumerable<TaggPreviewModel>>
    { }
