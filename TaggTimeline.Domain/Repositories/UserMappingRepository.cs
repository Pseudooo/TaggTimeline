
using Microsoft.EntityFrameworkCore;
using TaggTimeline.Domain.Context;
using TaggTimeline.Domain.Entities.Users;

namespace TaggTimeline.Domain.Repository;

public class UserMappingRepository : BaseRepository<UserMapping>
{
    public UserMappingRepository(DataContext context) : base(context)
        { }
    
    public Task<UserMapping?> GetByUserId(string userId)
    {
        return Context.UserMappings.SingleOrDefaultAsync(userMapping => userMapping.UserId == userId);
    }
}
