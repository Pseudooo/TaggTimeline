
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaggTimeline.Domain.Entities;
using TaggTimeline.Domain.Interface;

namespace TaggTimeline.Domain.Repository;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    internal DataContext Context { get; set; }

    public BaseRepository(DataContext context)
    {
        Context = context;
    }

    public Task<List<TEntity>> GetAll()
    {
        return Context.Set<TEntity>().ToListAsync();
    }

    public Task<TEntity?> GetById(Guid id)
    {
        return Context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id);
    }

    public Task<TEntity?> GetByIdWithNavigationProperties(Guid id, params Expression<Func<TEntity, object>>[] exprs)
    {
        var query = Context.Set<TEntity>().AsQueryable();
        query = exprs.Aggregate(query, (current, includes) => current.Include(includes));

        return query.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<TEntity> AddItem(TEntity entity)
    {
        await Context.AddAsync(entity);
        await Context.SaveChangesAsync(CancellationToken.None);

        return entity;
    }

    public Task SaveChanges(CancellationToken tok)
    {
        return Context.SaveChangesAsync(tok);
    }

}