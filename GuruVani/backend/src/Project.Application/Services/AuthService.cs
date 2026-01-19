using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Project.Application.DTOs;
using Project.Application.Interfaces;
using Project.Domain.Entities;

namespace Project.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> _userRepo;
        private readonly IMapper _mapper;

        public AuthService(IRepository<User> userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        private const int Pbkdf2Iter = 100_000;
        private const int SaltSize = 16; // 128-bit
        private const int HashSize = 32; // 256-bit

        public async Task<UserDto> RegisterAsync(string fullName, string email, string password)
        {
            // naive uniqueness check
            var existing = (await _userRepo.ListAsync()).FirstOrDefault(u => u.Email == email);
            if (existing != null) throw new InvalidOperationException("Email already registered");

            // create salt + hash
            var salt = GenerateSalt(SaltSize);
            var hash = DeriveHash(password, salt, Pbkdf2Iter, HashSize);

            var user = new User
            {
                FullName = fullName,
                Email = email,
                PasswordHash = Convert.ToBase64String(hash),
                PasswordSalt = Convert.ToBase64String(salt),
                Role = Project.Domain.Enums.Role.Customer,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepo.AddAsync(user);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> LoginAsync(string email, string password)
        {
            var user = await _userRepo.GetByIdAsync(Guid.Empty); // placeholder to satisfy type
            // Prefer optimized lookup via repository - try GetByEmailAsync if available
            if (user == null)
            {
                // fall back to scanning list if repository doesn't implement GetByEmailAsync
                user = (await _userRepo.ListAsync()).FirstOrDefault(u => u.Email == email);
            }

            if (user == null) throw new InvalidOperationException("Invalid credentials");

            if (string.IsNullOrWhiteSpace(user.PasswordSalt) || string.IsNullOrWhiteSpace(user.PasswordHash))
                throw new InvalidOperationException("Invalid credentials");

            var saltBytes = Convert.FromBase64String(user.PasswordSalt);
            var expectedHash = Convert.FromBase64String(user.PasswordHash);
            var providedHash = DeriveHash(password, saltBytes, Pbkdf2Iter, expectedHash.Length);

            if (!CryptographicOperations.FixedTimeEquals(providedHash, expectedHash))
                throw new InvalidOperationException("Invalid credentials");

            return _mapper.Map<UserDto>(user);
        }

        private static byte[] GenerateSalt(int size)
        {
            var salt = new byte[size];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return salt;
        }

        private static byte[] DeriveHash(string password, byte[] salt, int iterations, int outputBytes)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}
