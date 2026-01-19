using System.Collections.Generic;

namespace Guruvani.Domain.Entities;

public class GuruProfile
{
    public int GuruId { get; set; }
    public int UserId { get; set; }
    public string Expertise { get; set; } = string.Empty;
    public int ExperienceYears { get; set; }
    public bool IsVerified { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public User User { get; set; } = null!;
    public ICollection<AvailabilitySlot> AvailabilitySlots { get; set; } = new List<AvailabilitySlot>();
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}