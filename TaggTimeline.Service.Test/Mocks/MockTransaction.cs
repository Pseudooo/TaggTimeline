
using Moq;
using TaggTimeline.Domain.Interface;

namespace TaggTimeline.Service.Test.Mocks;

public class MockTransaction
{

    public static Mock<ITransactionWrapper> GetTransaction()
    {
        var mockTransaction = new Mock<ITransactionWrapper>();

        mockTransaction.Setup(trans => trans.Begin());
        mockTransaction.Setup(trans => trans.Commit());
        mockTransaction.Setup(trans => trans.Rollback());
        mockTransaction.Setup(trans => trans.DisposeAsync());

        return mockTransaction;
    }

}
