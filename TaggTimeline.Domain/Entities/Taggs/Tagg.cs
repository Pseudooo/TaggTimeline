namespace TaggTimeline.Domain.Entities.Taggs;

public class Tagg : KeyedEntity
{
    public IEnumerable<Instance> Instances { get; set; } = Enumerable.Empty<Instance>();
    public IEnumerable<Category> Categories { get; set; } = Enumerable.Empty<Category>();
}
