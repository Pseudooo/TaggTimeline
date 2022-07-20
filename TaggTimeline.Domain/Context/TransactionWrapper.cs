
using Microsoft.EntityFrameworkCore.Storage;
using TaggTimeline.Domain.Interface;

namespace TaggTimeline.Domain.Context;

public class TransactionWrapper : ITransactionWrapper
{
    private readonly DataContext _context;
    private IDbContextTransaction _transaction = null!;
    private bool _transactionClosed = false;

    public TransactionWrapper(DataContext context)
    {
        _context = context;
    }

    public async Task<ITransactionWrapper> Begin()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
        return this;
    }

    public async Task Commit()
    {
        if(!_transactionClosed)
        {
            _transactionClosed = true;
            await _transaction.CommitAsync();
        }
    }

    public async Task Rollback()
    {
        if(!_transactionClosed)
        {
            _transactionClosed = true;
            await _transaction.RollbackAsync();
        }
    }

    public async ValueTask DisposeAsync()
    {
        await Rollback();
    }
}
