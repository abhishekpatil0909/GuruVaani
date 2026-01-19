namespace Project.Domain.Enums
{
    public enum Role
    {
        Admin,
        Guru,
        Customer
    }

    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Completed
    }

    public enum PaymentStatus
    {
        Pending,
        Paid,
        Failed,
        Refunded
    }
}
