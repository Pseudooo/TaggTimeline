
using System.Linq.Expressions;
using TaggTimeline.Domain.Entities;

namespace TaggTimeline.Domain.Interface;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<List<TEntity>> GetAll();
    Task<List<TEntity_>> GetAllFromUser<TEntity_>(string userId) where TEntity_ : TEntity, IUserOwnedEntity;
    Task<TEntity?> GetById(Guid id);
    Task<TEntity> AddItem(TEntity entity);
    Task<TEntity?> GetByIdWithNavigationProperties(Guid id, params Expression<Func<TEntity, object>>[] exprs);
    Task SaveChanges(CancellationToken tok);
}
