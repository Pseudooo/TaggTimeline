
namespace TaggTimeline.Domain.Entities;

public class MutableDatedEntity : DatedEntity
{
    public DateTime? ModifiedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
}
