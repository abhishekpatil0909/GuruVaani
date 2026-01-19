using System;

namespace Project.Domain.Entities
{
    public class GuruProfile : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Expertise { get; set; } = string.Empty;
        public int ExperienceYears { get; set; }
        public bool IsVerified { get; set; }

        public User? User { get; set; }
    }
}
