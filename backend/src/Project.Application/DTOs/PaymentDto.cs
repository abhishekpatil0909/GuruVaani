using System;
using Project.Domain.Enums;

namespace Project.Application.DTOs
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
