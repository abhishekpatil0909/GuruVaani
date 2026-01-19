using System;

namespace Project.Application.DTOs
{
    public class AvailabilitySlotDto
    {
        public Guid Id { get; set; }
        public Guid GuruId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsBooked { get; set; }
    }
}
