using System;
using Project.Domain.Enums;

namespace Project.Domain.Entities
{
    public class Booking : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public Guid GuruId { get; set; }
        public Guid SlotId { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        public User? Customer { get; set; }
        public User? Guru { get; set; }
        public AvailabilitySlot? Slot { get; set; }
    }
}
