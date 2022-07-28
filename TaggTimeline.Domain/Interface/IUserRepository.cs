
using TaggTimeline.Domain.Entities.Users;

namespace TaggTimeline.Domain.Interface;

public interface IUserRepository : IBaseRepository<UserMapping>
{
    Task<UserMapping?> GetByUserId(string userId);
}
