using System;
using Project.Domain.Enums;

namespace Project.Application.DTOs
{
    public class BookingDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid GuruId { get; set; }
        public Guid SlotId { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
