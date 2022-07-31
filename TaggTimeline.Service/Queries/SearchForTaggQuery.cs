using TaggTimeline.ClientModel.Taggs;

namespace TaggTimeline.Service.Queries;

public class SearchForTaggQuery : UserCentricRequest<IEnumerable<TaggPreviewModel>>
{
    public string SearchTerm { get; set; } = null!;
}
