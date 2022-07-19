
using MediatR;
using TaggTimeline.ClientModel.Taggs;

namespace TaggTimeline.Service.Queries;

public class GetAllTaggsQuery : IRequest<IEnumerable<TaggPreviewModel>>
    { }
