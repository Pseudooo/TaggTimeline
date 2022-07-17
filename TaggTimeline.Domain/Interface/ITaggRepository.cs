
using TaggTimeline.Domain.Entities.Taggs;

namespace TaggTimeline.Domain.Interface;

public interface ITaggRepository : IBaseRepository<Tagg>
{
    Task<IEnumerable<Tagg>> SearchForTagg(string searchTerm);
}
