
namespace TaggTimeline.Domain.Entities.Taggs;

public class Category : KeyedEntity
{
    public IEnumerable<Tagg> Taggs { get; set; } = null!;
}
