
using TaggTimeline.Domain.Entities;

namespace TaggTimeline.Domain.Interface;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    public Task<TEntity?> GetById(Guid id);
    Task<TEntity> AddItem(TEntity entity);
}
