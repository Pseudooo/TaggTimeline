
namespace TaggTimeline.Domain.Interface;

public interface ITransaction
{
    Task InitialiseTransaction();
    Task CommitChanges();
    ValueTask DisposeAsync();
}
