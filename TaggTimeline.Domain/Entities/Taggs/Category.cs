
namespace TaggTimeline.Domain.Entities.Taggs;

public class Category : KeyedEntity, IUserOwnedEntity
{
    public IEnumerable<Tagg> Taggs { get; set; } = null!;
    public string UserId { get; set; } = null!;
}
