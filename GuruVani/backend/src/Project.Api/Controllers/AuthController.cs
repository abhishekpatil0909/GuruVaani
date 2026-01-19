using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Project.Application.Interfaces;
using Project.Application.DTOs;

namespace Project.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        private readonly IConfiguration _config;

        public AuthController(IAuthService auth, IConfiguration config)
        {
            _auth = auth;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest req)
        {
            // Map to application validator model
            var appReq = new Project.Application.Validators.RegisterRequest
            {
                FullName = req.FullName,
                Email = req.Email,
                Password = req.Password
            };

            var validator = new Project.Application.Validators.RegisterValidator();
            var result = validator.Validate(appReq);
            if (!result.IsValid) return BadRequest(result.Errors);

            var user = await _auth.RegisterAsync(req.FullName, req.Email, req.Password);
            return CreatedAtAction(null, user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var user = await _auth.LoginAsync(req.Email, req.Password);
            if (user == null) return Unauthorized();

            // produce a real JWT using configuration with standard claims
            var jwt = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.GetValue<string>("Key")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim("role", user.Role.ToString())
            };

            var handler = new JwtSecurityTokenHandler();
            var secToken = new JwtSecurityToken(
                issuer: jwt.GetValue<string>("Issuer"),
                audience: jwt.GetValue<string>("Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwt.GetValue<int>("ExpiresMinutes")),
                signingCredentials: creds
            );

            var tokenString = handler.WriteToken(secToken);

            return Ok(new { token = tokenString, user });
        }
    }

    public class RegisterRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
