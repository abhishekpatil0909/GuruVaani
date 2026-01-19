using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Domain.Entities;

namespace Project.Application.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<IEnumerable<Booking>> ListByCustomerIdAsync(Guid customerId);
        Task<IEnumerable<Booking>> ListByGuruIdAsync(Guid guruId);
        Task<Booking?> GetByIdWithDetailsAsync(Guid id);
    }
}
