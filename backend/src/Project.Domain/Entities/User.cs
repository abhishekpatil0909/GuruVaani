using System;
using Project.Domain.Enums;

namespace Project.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        // PBKDF2 salted password storage
        public string PasswordHash { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public Role Role { get; set; }

        public GuruProfile? GuruProfile { get; set; }
        public CustomerProfile? CustomerProfile { get; set; }
    }
}
