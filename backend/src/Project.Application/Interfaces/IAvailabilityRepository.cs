using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Domain.Entities;

namespace Project.Application.Interfaces
{
    public interface IAvailabilityRepository : IRepository<AvailabilitySlot>
    {
        Task<IEnumerable<AvailabilitySlot>> ListByGuruIdAsync(Guid guruId);
        Task<AvailabilitySlot?> GetAvailableSlotAsync(Guid slotId);
    }
}
