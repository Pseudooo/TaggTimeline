namespace TaggTimeline.Domain.Entities.Taggs;

public class Tagg : KeyedEntity
{
    public IEnumerable<Instance> Instances { get; set; } = null!;
    public IEnumerable<Category> Categories { get; set; } = null!;
}
