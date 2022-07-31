
using System.ComponentModel.DataAnnotations;
using TaggTimeline.ClientModel.Taggs;

namespace TaggTimeline.Service.Queries;

public class SearchForCategoriesQuery : UserCentricRequest<IEnumerable<CategoryPreviewModel>>
{
    [Required]
    public string SearchTerm { get; set; } = null!;
}
