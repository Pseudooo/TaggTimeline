
using TaggTimeline.Domain.Entities;

namespace TaggTimeline.Domain.Interface;

public interface IKeyedEntityRepository<TEntity> : IBaseRepository<TEntity> where TEntity : KeyedEntity, IUserOwnedEntity
{
    Task<IEnumerable<TEntity>> SearchForKey(string searchTerm);
    Task<IEnumerable<TEntity>> SearchForKeyFromUser(string searchTerm, string userId);
}
