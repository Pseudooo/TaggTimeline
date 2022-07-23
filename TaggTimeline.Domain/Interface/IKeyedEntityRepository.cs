
using TaggTimeline.Domain.Entities;

namespace TaggTimeline.Domain.Interface;

public interface IKeyedEntityRepository<TEntity> : IBaseRepository<TEntity> where TEntity : KeyedEntity
{
    Task<IEnumerable<TEntity>> SearchForKey(string searchTerm);
}
