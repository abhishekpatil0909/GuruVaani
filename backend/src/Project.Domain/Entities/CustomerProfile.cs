using System;

namespace Project.Domain.Entities
{
    public class CustomerProfile : BaseEntity
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
