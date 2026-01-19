using System;

namespace Project.Domain.Entities
{
    public class AvailabilitySlot : BaseEntity
    {
        public Guid GuruId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsBooked { get; set; }

        public User? Guru { get; set; }
    }
}
