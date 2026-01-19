using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Application.DTOs;

namespace Project.Application.Interfaces
{
    public interface IBookingService
    {
        Task<BookingDto> CreateAsync(Guid customerId, Guid slotId);
        Task<IEnumerable<BookingDto>> ListByCustomerAsync(Guid customerId);
        Task<IEnumerable<BookingDto>> ListByGuruAsync(Guid guruId);
        Task<BookingDto?> GetByIdAsync(Guid id);
    }
}
