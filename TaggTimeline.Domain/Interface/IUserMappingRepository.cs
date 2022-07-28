
using TaggTimeline.Domain.Entities.Users;

namespace TaggTimeline.Domain.Interface;

public interface IUserMappingRepository : IBaseRepository<UserMapping>
{
    Task<UserMapping> GetByKnownUserId(string userId);
    Task<UserMapping> CreateUserMapping(string userId);
}
