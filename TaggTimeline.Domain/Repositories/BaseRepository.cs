
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaggTimeline.Domain.Entities;
using TaggTimeline.Domain.Interface;

namespace TaggTimeline.Domain.Repository;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DataContext _context;

    public BaseRepository(DataContext context)
    {
        _context = context;
    }

    public Task<TEntity?> GetById(Guid id)
    {
        return _context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id);
    }

    public Task<TEntity?> GetByIdWithNavigationProperties(Guid id, params Expression<Func<TEntity, object>>[] exprs)
    {
        var query = _context.Set<TEntity>().AsQueryable();
        query = exprs.Aggregate(query, (current, includes) => current.Include(includes));

        return query.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<TEntity> AddItem(TEntity entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync(CancellationToken.None);

        return entity;
    }

    public Task SaveChanges(CancellationToken tok)
    {
        return _context.SaveChangesAsync(tok);
    }

}