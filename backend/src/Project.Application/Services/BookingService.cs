using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Project.Application.Interfaces;
using Project.Application.DTOs;
using Project.Application.Interfaces;
using Project.Domain.Entities;
using Project.Domain.Enums;

namespace Project.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<Booking> _repo;
        private readonly IRepository<AvailabilitySlot> _slotRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IPaymentService _paymentService;

        public BookingService(IRepository<Booking> repo, IRepository<AvailabilitySlot> slotRepo, IMapper mapper, IUnitOfWork uow, IPaymentService paymentService)
        {
            _repo = repo;
            _slotRepo = slotRepo;
            _mapper = mapper;
            _uow = uow;
            _paymentService = paymentService;
        }

        public async Task<BookingDto> CreateAsync(Guid customerId, Guid slotId)
        {
            // Wrap slot update + booking creation + payment creation in a single DB transaction
            await using var tx = await _uow.BeginTransactionAsync();
            try
            {
                var slot = await _slotRepo.GetByIdAsync(slotId);
                if (slot == null) throw new InvalidOperationException("Slot not found");
                if (slot.IsBooked) throw new InvalidOperationException("Slot already booked");

                var booking = new Booking
                {
                    CustomerId = customerId,
                    GuruId = slot.GuruId,
                    SlotId = slotId,
                    Status = BookingStatus.Confirmed,
                    CreatedAt = DateTime.UtcNow
                };

                // mark slot as booked
                slot.IsBooked = true;
                await _slotRepo.UpdateAsync(slot);

                // create booking
                await _repo.AddAsync(booking);

                // create payment for booking (amount currently defaulted to 0)
                // PaymentService will use the same DbContext via DI so this will participate in the transaction
                await _paymentService.CreateForBookingAsync(booking.Id, 0m);

                await tx.CommitAsync();

                return _mapper.Map<BookingDto>(booking);
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<BookingDto>> ListByCustomerAsync(Guid customerId)
        {
            var all = await _repo.ListAsync();
            var items = all.Where(x => x.CustomerId == customerId);
            return items.Select(x => _mapper.Map<BookingDto>(x));
        }

        public async Task<IEnumerable<BookingDto>> ListByGuruAsync(Guid guruId)
        {
            var all = await _repo.ListAsync();
            var items = all.Where(x => x.GuruId == guruId);
            return items.Select(x => _mapper.Map<BookingDto>(x));
        }

        public async Task<BookingDto?> GetByIdAsync(Guid id)
        {
            var b = await _repo.GetByIdAsync(id);
            return b == null ? null : _mapper.Map<BookingDto>(b);
        }
    }
}
