using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Project.Application.Interfaces;
using Project.Infrastructure.Data;

namespace Project.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IUnitOfWorkTransaction> BeginTransactionAsync()
        {
            var tx = await _db.Database.BeginTransactionAsync();
            return new UnitOfWorkTransaction(tx);
        }
    }
}
