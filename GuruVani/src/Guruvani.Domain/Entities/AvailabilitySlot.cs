namespace Guruvani.Domain.Entities;

public class AvailabilitySlot
{
    public int SlotId { get; set; }
    public int GuruId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsBooked { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public GuruProfile Guru { get; set; } = null!;
    public Booking? Booking { get; set; }
}