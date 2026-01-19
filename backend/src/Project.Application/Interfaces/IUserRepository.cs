using System;
using System.Threading.Tasks;
using Project.Domain.Entities;

namespace Project.Application.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdWithProfilesAsync(Guid id);
    }
}
