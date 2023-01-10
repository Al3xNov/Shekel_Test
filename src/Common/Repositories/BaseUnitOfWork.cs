using Common.EF;
using Common.Interfaces;

namespace Common.Repositories;
public class BaseUnitOfWork : IBaseUnitOfWork
{
    private readonly TestContext _context;
    public BaseUnitOfWork(TestContext context)
    {
        _context = context;
    }

    #region Methods

    /// <summary>
    /// Completes the unit of work, saving all repository changes to the underlying data-store.
    /// </summary>
    /// <returns><see cref="Task"/></returns>
    public async Task CompleteAsync() => await _context.SaveChangesAsync();

    #endregion

    #region Implements IDisposable

    #region Private Dispose Fields

    private bool _disposed;

    #endregion

    /// <summary>
    /// Cleans up any resources being used.
    /// </summary>
    /// <returns><see cref="ValueTask"/></returns>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);

        // Take this object off the finalization queue to prevent 
        // finalization code for this object from executing a second time.
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Cleans up any resources being used.
    /// </summary>
    /// <param name="disposing">Whether or not we are disposing</param> 
    /// <returns><see cref="ValueTask"/></returns>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Dispose managed resources.
                await _context.DisposeAsync();
            }

            // Dispose any unmanaged resources here...

            _disposed = true;
        }
    }
    #endregion
}