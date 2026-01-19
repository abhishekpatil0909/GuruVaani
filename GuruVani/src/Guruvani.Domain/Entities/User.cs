namespace Guruvani.Domain.Entities;

public class User
{
    public int UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty; // Admin, Guru, Customer
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public GuruProfile? GuruProfile { get; set; }
    public CustomerProfile? CustomerProfile { get; set; }
}
