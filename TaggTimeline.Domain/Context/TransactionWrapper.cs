
using Microsoft.EntityFrameworkCore.Storage;
using TaggTimeline.Domain.Interface;

namespace TaggTimeline.Domain.Context;

public class TransactionWrapper : ITransactionWrapper
{
    private readonly DataContext _context;
    private IDbContextTransaction _transaction = null!;

    public TransactionWrapper(DataContext context)
    {
        _context = context;
    }

    public async Task<ITransactionWrapper> Begin()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
        return this;
    }

    public Task Commit()
    {
        return _transaction.CommitAsync();
    }

    public Task Rollback()
    {
        return _transaction.RollbackAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await Rollback();
    }
}
