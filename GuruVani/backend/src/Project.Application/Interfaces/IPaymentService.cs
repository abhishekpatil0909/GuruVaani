using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Application.DTOs;

namespace Project.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentDto> CreateForBookingAsync(Guid bookingId, decimal amount);
        Task<PaymentDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<PaymentDto>> GetByCustomerIdAsync(Guid customerId);
    }
}
