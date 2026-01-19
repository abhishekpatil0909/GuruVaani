using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Project.Application.DTOs;
using Project.Application.Interfaces;
using Project.Domain.Entities;
using Project.Domain.Enums;

namespace Project.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<Payment> _repo;
        private readonly IRepository<Booking> _bookingRepo;
        private readonly IPaymentRepository _paymentRepo;
        private readonly IMapper _mapper;

        public PaymentService(IRepository<Payment> repo, IRepository<Booking> bookingRepo, IPaymentRepository paymentRepo, IMapper mapper)
        {
            _repo = repo;
            _bookingRepo = bookingRepo;
            _paymentRepo = paymentRepo;
            _mapper = mapper;
        }

        public async Task<PaymentDto> CreateForBookingAsync(Guid bookingId, decimal amount)
        {
            var booking = await _bookingRepo.GetByIdAsync(bookingId);
            if (booking == null) throw new InvalidOperationException("Booking not found");

            var p = new Payment
            {
                BookingId = bookingId,
                Amount = amount,
                Status = PaymentStatus.Paid,
                CreatedAt = DateTime.UtcNow
            };

            await _repo.AddAsync(p);
            return _mapper.Map<PaymentDto>(p);
        }

        public async Task<PaymentDto?> GetByIdAsync(Guid id)
        {
            var p = await _repo.GetByIdAsync(id);
            return p == null ? null : _mapper.Map<PaymentDto>(p);
        }

        public async Task<IEnumerable<PaymentDto>> GetByCustomerIdAsync(Guid customerId)
        {
            return await _paymentRepo.GetByCustomerIdAsync(customerId);
        }
    }
}
