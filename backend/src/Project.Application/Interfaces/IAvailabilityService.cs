using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Application.DTOs;

namespace Project.Application.Interfaces
{
    public interface IAvailabilityService
    {
        Task<AvailabilitySlotDto> CreateAsync(AvailabilitySlotDto dto);
        Task<IEnumerable<AvailabilitySlotDto>> ListByGuruAsync(Guid guruId);
    }
}
