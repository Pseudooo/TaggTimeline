
using TaggTimeline.Domain.Entities.Users;

namespace TaggTimeline.Domain.Interface;

public interface IUserMappingRepository : IBaseRepository<UserMapping>
{
    Task<UserMapping?> GetByUserId(string userId);
}
