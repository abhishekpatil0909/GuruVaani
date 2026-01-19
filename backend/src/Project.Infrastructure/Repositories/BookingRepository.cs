using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Application.Interfaces;
using Project.Domain.Entities;
using Project.Infrastructure.Data;

namespace Project.Infrastructure.Repositories
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> ListByCustomerIdAsync(Guid customerId)
        {
            return await _context.Bookings
                .Include(b => b.Slot)
                .Where(b => b.CustomerId == customerId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> ListByGuruIdAsync(Guid guruId)
        {
            return await _context.Bookings
                .Include(b => b.Slot)
                .Where(b => b.GuruId == guruId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<Booking?> GetByIdWithDetailsAsync(Guid id)
        {
            return await _context.Bookings
                .Include(b => b.Slot)
                .Include(b => b.Customer)
                .Include(b => b.Guru)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
