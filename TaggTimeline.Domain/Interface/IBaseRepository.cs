
using System.Linq.Expressions;
using TaggTimeline.Domain.Entities;

namespace TaggTimeline.Domain.Interface;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    public Task<TEntity?> GetById(Guid id);
    Task<TEntity> AddItem(TEntity entity);
    public Task<TEntity?> GetByIdWithNavigationProperties(Guid id, params Expression<Func<TEntity, object>>[] exprs);
    Task SaveChanges(CancellationToken tok);
}
