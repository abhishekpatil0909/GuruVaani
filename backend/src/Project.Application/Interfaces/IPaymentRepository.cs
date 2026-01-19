using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Application.DTOs;

namespace Project.Application.Interfaces
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<PaymentDto>> GetByCustomerIdAsync(Guid customerId);
    }
}