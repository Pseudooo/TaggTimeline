
using TaggTimeline.Domain.Context;

namespace TaggTimeline.Domain.Interface;

public interface ITransactionWrapper : IAsyncDisposable
{
    Task<ITransactionWrapper> Begin();
    Task Commit();
    Task Rollback();
}
