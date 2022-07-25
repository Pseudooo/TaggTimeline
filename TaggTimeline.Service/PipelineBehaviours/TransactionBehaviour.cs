
using MediatR;
using TaggTimeline.Domain.Interface;

namespace TaggTimeline.Service.PipelineBehaviours;

public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ITransactionWrapper _transactionWrapper;

    public TransactionBehaviour(ITransactionWrapper transactionWrapper)
    {
        _transactionWrapper = transactionWrapper;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var transaction = await _transactionWrapper.Begin();
        TResponse response;

        try
        {
            response = await next();

            await _transactionWrapper.Commit();
        }
        finally
        {
            await _transactionWrapper.Rollback();
        }

        return response;
    }
}
