using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;

namespace Project.Infrastructure.Data
{
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

            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Email).IsRequired();
                b.HasIndex(x => x.Email).IsUnique();
                b.HasOne(x => x.GuruProfile).WithOne(g => g.User).HasForeignKey<GuruProfile>(g => g.UserId);
                b.HasOne(x => x.CustomerProfile).WithOne(c => c.User).HasForeignKey<CustomerProfile>(c => c.UserId);
            });

            modelBuilder.Entity<GuruProfile>(b =>
            {
                b.HasKey(x => x.Id);
            });

            modelBuilder.Entity<CustomerProfile>(b =>
            {
                b.HasKey(x => x.Id);
            });

            modelBuilder.Entity<AvailabilitySlot>(b =>
            {
                b.HasKey(x => x.Id);
                b.HasOne<User>().WithMany().HasForeignKey(x => x.GuruId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Booking>(b =>
            {
                b.HasKey(x => x.Id);
                b.HasOne(x => x.Slot).WithMany().HasForeignKey(x => x.SlotId).OnDelete(DeleteBehavior.Restrict);
                b.HasOne<User>().WithMany().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Cascade);
                b.HasOne<User>().WithMany().HasForeignKey(x => x.GuruId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Payment>(b =>
            {
                b.HasKey(x => x.Id);
                b.HasOne(x => x.Booking).WithMany().HasForeignKey(x => x.BookingId).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
