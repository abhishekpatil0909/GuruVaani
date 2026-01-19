using Guruvani.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Guruvani.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<GuruProfile> GuruProfiles => Set<GuruProfile>();
    public DbSet<CustomerProfile> CustomerProfiles => Set<CustomerProfile>();
    public DbSet<AvailabilitySlot> AvailabilitySlots => Set<AvailabilitySlot>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configuration
        modelBuilder.Entity<User>(b =>
        {
            b.HasKey(u => u.UserId);
            b.Property(u => u.FullName).IsRequired().HasMaxLength(100);
            b.Property(u => u.Email).IsRequired().HasMaxLength(255);
            b.Property(u => u.PasswordHash).IsRequired();
            b.Property(u => u.Role).IsRequired().HasMaxLength(50);
            b.HasIndex(u => u.Email).IsUnique();
        });

        // GuruProfile configuration
        modelBuilder.Entity<GuruProfile>(b =>
        {
            b.HasKey(g => g.GuruId);
            b.Property(g => g.Expertise).IsRequired().HasMaxLength(200);
            b.Property(g => g.ExperienceYears).IsRequired();
            b.HasOne(g => g.User)
                .WithOne(u => u.GuruProfile)
                .HasForeignKey<GuruProfile>(g => g.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // CustomerProfile configuration
        modelBuilder.Entity<CustomerProfile>(b =>
        {
            b.HasKey(c => c.CustomerId);
            b.HasOne(c => c.User)
                .WithOne(u => u.CustomerProfile)
                .HasForeignKey<CustomerProfile>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // AvailabilitySlot configuration
        modelBuilder.Entity<AvailabilitySlot>(b =>
        {
            b.HasKey(a => a.SlotId);
            b.Property(a => a.StartTime).IsRequired();
            b.Property(a => a.EndTime).IsRequired();
            b.HasOne(a => a.Guru)
                .WithMany(g => g.AvailabilitySlots)
                .HasForeignKey(a => a.GuruId)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasOne(a => a.Booking)
                .WithOne(b => b.Slot)
                .HasForeignKey<Booking>(b => b.SlotId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Booking configuration
        modelBuilder.Entity<Booking>(b =>
        {
            b.HasKey(bk => bk.BookingId);
            b.Property(bk => bk.Status).IsRequired().HasMaxLength(50);
            b.HasOne(bk => bk.Customer)
                .WithMany(c => c.Bookings)
                .HasForeignKey(bk => bk.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasOne(bk => bk.Guru)
                .WithMany(g => g.Bookings)
                .HasForeignKey(bk => bk.GuruId)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasOne(bk => bk.Slot)
                .WithOne(a => a.Booking)
                .HasForeignKey<Booking>(bk => bk.SlotId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasOne(bk => bk.Payment)
                .WithOne(p => p.Booking)
                .HasForeignKey<Payment>(p => p.BookingId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Payment configuration
        modelBuilder.Entity<Payment>(b =>
        {
            b.HasKey(p => p.PaymentId);
            b.Property(p => p.Amount).HasColumnType("decimal(18,2)");
            b.Property(p => p.Status).IsRequired().HasMaxLength(50);
            b.HasOne(p => p.Booking)
                .WithOne(b => b.Payment)
                .HasForeignKey<Payment>(p => p.BookingId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
