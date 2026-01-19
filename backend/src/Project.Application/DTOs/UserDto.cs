using System;
using Project.Domain.Enums;

namespace Project.Application.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
