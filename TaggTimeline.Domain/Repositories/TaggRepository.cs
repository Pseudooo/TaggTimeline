
using Microsoft.EntityFrameworkCore;
using TaggTimeline.Domain.Context;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;

namespace TaggTimeline.Domain.Repository;

public class TaggRepository : BaseRepository<Tagg>, ITaggRepository
{
    public TaggRepository(DataContext context) : base(context)
        { }

    public async Task<IEnumerable<Tagg>> SearchForTagg(string searchTerm)
    {
        var result = await Context.Taggs.Where(x => EF.Functions.Like(x.Key, $"%{searchTerm}%")).ToListAsync();
        return result;
    }

}
