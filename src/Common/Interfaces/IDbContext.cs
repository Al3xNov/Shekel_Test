using Microsoft.EntityFrameworkCore;

namespace Common.Interfaces;
public interface IDbContext : IDisposable
{
    DbContext Instance { get; }
}