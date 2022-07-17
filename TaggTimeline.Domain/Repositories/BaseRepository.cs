
using Microsoft.EntityFrameworkCore;
using TaggTimeline.Domain.Entities;
using TaggTimeline.Domain.Interface;

namespace TaggTimeline.Domain.Repository;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly DataContext _context;

    public BaseRepository(DataContext context)
    {
        _context = context;
    }

    public Task<T?> GetById(Guid id)
    {
        return _context.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
    }

}