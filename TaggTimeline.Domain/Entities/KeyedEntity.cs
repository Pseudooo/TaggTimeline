
namespace TaggTimeline.Domain.Entities;

public class KeyedEntity : DatedEntity
{
    public string Key { get; set; } = null!;
}
