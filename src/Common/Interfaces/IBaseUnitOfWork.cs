using Microsoft.EntityFrameworkCore;

namespace Common.Interfaces;
public interface IBaseUnitOfWork : IAsyncDisposable
{
    #region Methods

    Task CompleteAsync();

    #endregion
}