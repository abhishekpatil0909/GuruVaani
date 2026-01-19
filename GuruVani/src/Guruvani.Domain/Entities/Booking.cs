namespace Guruvani.Domain.Entities;

public class Booking
{
    public int BookingId { get; set; }
    public int CustomerId { get; set; }
    public int GuruId { get; set; }
    public int SlotId { get; set; }
    public string Status { get; set; } = string.Empty; // Pending, Confirmed, Cancelled
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public CustomerProfile Customer { get; set; } = null!;
    public GuruProfile Guru { get; set; } = null!;
    public AvailabilitySlot Slot { get; set; } = null!;
    public Payment? Payment { get; set; }
}