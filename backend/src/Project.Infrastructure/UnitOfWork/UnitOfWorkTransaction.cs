using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Project.Application.Interfaces;

namespace Project.Infrastructure.UnitOfWork
{
    internal class UnitOfWorkTransaction : IUnitOfWorkTransaction
    {
        private readonly IDbContextTransaction _tx;

        public UnitOfWorkTransaction(IDbContextTransaction tx)
        {
            _tx = tx;
        }

        public async Task CommitAsync() => await _tx.CommitAsync();

        public async Task RollbackAsync() => await _tx.RollbackAsync();

        public async ValueTask DisposeAsync() => await _tx.DisposeAsync();
    }
}
