using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Application.Interfaces;
using Project.Domain.Entities;
using Project.Infrastructure.Data;

namespace Project.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.GuruProfile)
                .Include(u => u.CustomerProfile)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdWithProfilesAsync(Guid id)
        {
            return await _context.Users
                .Include(u => u.GuruProfile)
                .Include(u => u.CustomerProfile)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
