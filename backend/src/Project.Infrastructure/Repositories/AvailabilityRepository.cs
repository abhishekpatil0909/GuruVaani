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
    public class AvailabilityRepository : Repository<AvailabilitySlot>, IAvailabilityRepository
    {
        private readonly AppDbContext _context;

        public AvailabilityRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AvailabilitySlot>> ListByGuruIdAsync(Guid guruId)
        {
            return await _context.AvailabilitySlots
                .Where(s => s.GuruId == guruId)
                .OrderBy(s => s.StartTime)
                .ToListAsync();
        }

        public async Task<AvailabilitySlot?> GetAvailableSlotAsync(Guid slotId)
        {
            return await _context.AvailabilitySlots
                .FirstOrDefaultAsync(s => s.Id == slotId && !s.IsBooked);
        }
    }
}
