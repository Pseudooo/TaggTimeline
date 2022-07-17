
using TaggTimeline.Domain.Entities;

namespace TaggTimeline.Domain.Interface;

public interface IBaseRepository<T> where T : BaseEntity
{
    public Task<T?> GetById(Guid id);
}
