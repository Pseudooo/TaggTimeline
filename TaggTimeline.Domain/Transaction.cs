
using Microsoft.EntityFrameworkCore.Storage;
using TaggTimeline.Domain.Interface;

namespace TaggTimeline.Domain;

public class Transaction : IAsyncDisposable, ITransaction
{
    
    private readonly DataContext _context;
    private IDbContextTransaction _transaction = null!;

    public Transaction(DataContext context)
    {
        _context = context;
    }

    public async Task InitialiseTransaction()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public Task CommitChanges()
    {
        return _transaction.CommitAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _transaction.RollbackAsync();
    }
}
