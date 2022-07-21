
namespace TaggTimeline.Domain.Entities;

public class KeyedEntity : MutableDatedEntity
{
    public string Key { get; set; } = null!;
}
