
using Microsoft.EntityFrameworkCore;
using TaggTimeline.Domain.Context;
using TaggTimeline.Domain.Entities.Users;
using TaggTimeline.Domain.Interface;

namespace TaggTimeline.Domain.Repository;

public class UserMappingRepository : BaseRepository<UserMapping>, IUserMappingRepository
{
    public UserMappingRepository(DataContext context) : base(context)
        { }

    public Task<UserMapping> GetByKnownUserId(string userId)
    {
        return Context.UserMappings.SingleAsync(userMapping => userMapping.UserId == userId);
    }

    public async Task<UserMapping> CreateUserMapping(string userId)
    {
        var toBeCreated = new UserMapping()
        {
            Id = Guid.NewGuid(),
            UserId = userId,
        };

        await Context.UserMappings.AddAsync(toBeCreated);
        await Context.SaveChangesAsync(CancellationToken.None);

        return toBeCreated;
    }
}
