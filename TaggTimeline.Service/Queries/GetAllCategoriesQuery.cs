using TaggTimeline.ClientModel.Taggs;

namespace TaggTimeline.Service.Queries;

public class GetAllCategoriesQuery : UserCentricRequest<IEnumerable<CategoryPreviewModel>>
    { }
