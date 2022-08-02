
using Microsoft.EntityFrameworkCore;
using TaggTimeline.Domain.Context;
using TaggTimeline.Domain.Entities;
using TaggTimeline.Domain.Interface;

namespace TaggTimeline.Domain.Repository;

public class KeyedEntityRepository<TEntity> : BaseRepository<TEntity>, IKeyedEntityRepository<TEntity> where TEntity : KeyedEntity, IUserOwnedEntity
{
    public KeyedEntityRepository(DataContext context) : base(context)
        { }

    public async Task<IEnumerable<TEntity>> SearchForKeyFromUser(string searchTerm, string userId)
    {
        var result = await Context.Set<TEntity>()
                                  .Where(entity => EF.Functions.Like(entity.Key, $"%{searchTerm}%"))
                                  .Where(entity => entity.UserId == userId)
                                  .ToListAsync();
        return result;
    }

}
