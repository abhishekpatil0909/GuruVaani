using System;
using System.Threading.Tasks;

namespace Project.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task<IUnitOfWorkTransaction> BeginTransactionAsync();
    }

    public interface IUnitOfWorkTransaction : IAsyncDisposable
    {
        Task CommitAsync();
        Task RollbackAsync();
    }
}
