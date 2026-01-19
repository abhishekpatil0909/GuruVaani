using System.Collections.Generic;

namespace Guruvani.Domain.Entities;

public class CustomerProfile
{
    public int CustomerId { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public User User { get; set; } = null!;
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}