
using MediatR;
using TaggTimeline.ClientModel.Taggs;

namespace TaggTimeline.Service.Commands;

public class AddCategoryToTaggCommand : UserCentricRequest<TaggModel>
{
    public Guid TaggId { get; set; }
    public Guid CategoryId { get; set; }
}
