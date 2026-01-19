using System;
using Project.Domain.Enums;

namespace Project.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid BookingId { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        public Booking? Booking { get; set; }
    }
}
