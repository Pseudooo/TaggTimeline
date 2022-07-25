
using MediatR;
using TaggTimeline.ClientModel.Taggs;

namespace TaggTimeline.Service.Commands;

public class AddCategoryToTaggCommand : IRequest<TaggModel>
{
    public Guid TaggId { get; set; }
    public Guid CategoryId { get; set; }
}
