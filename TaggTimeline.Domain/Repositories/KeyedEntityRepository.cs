
using Microsoft.EntityFrameworkCore;
using TaggTimeline.Domain.Context;
using TaggTimeline.Domain.Entities;

namespace TaggTimeline.Domain.Repository;

public class KeyedEntityRepository<TEntity> : BaseRepository<TEntity> where TEntity : KeyedEntity
{
    public KeyedEntityRepository(DataContext context) : base(context)
        { }

    public async Task<IEnumerable<TEntity>> SearchForKey(string searchTerm)
    {
        var result = await Context.Set<TEntity>()
                                  .Where(entity => EF.Functions.Like(entity.key, $"%{searchTerm}%"))
                                  .ToListAsync();
        return result;
    }

}
