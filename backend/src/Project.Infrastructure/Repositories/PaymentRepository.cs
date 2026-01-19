using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.DTOs;
using Project.Application.Interfaces;
using Project.Domain.Entities;
using Project.Infrastructure.Data;

namespace Project.Infrastructure.Repositories
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        private readonly IMapper _mapper;

        public PaymentRepository(AppDbContext db, IMapper mapper) : base(db)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<PaymentDto>> GetByCustomerIdAsync(Guid customerId)
        {
            var payments = await _set
                .Include(p => p.Booking)
                .Where(p => p.Booking != null && p.Booking.CustomerId == customerId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }
    }
}