using System;

namespace Project.Application.DTOs
{
    public class GuruProfileDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Expertise { get; set; } = string.Empty;
        public int ExperienceYears { get; set; }
        public bool IsVerified { get; set; }
    }
}
