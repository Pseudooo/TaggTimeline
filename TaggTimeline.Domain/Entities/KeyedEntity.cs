
namespace TaggTimeline.Domain.Entities;

public class KeyedEntity : DatedEntity
{
    public string key { get; set; } = null!;
}
