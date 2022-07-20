
using Moq;
using TaggTimeline.Domain.Interface;

namespace TaggTimeline.Service.Test.Mocks;

public class MockTransaction
{

    public static Mock<ITransaction> GetTransaction()
    {
        var mockTransaction = new Mock<ITransaction>();

        mockTransaction.Setup(trans => trans.InitialiseTransaction());
        mockTransaction.Setup(trans => trans.CommitChanges());
        mockTransaction.Setup(trans => trans.DisposeAsync());

        return mockTransaction;
    }

}
