using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Interfaces;
using Project.Application.DTOs;

namespace Project.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GuruController : ControllerBase
    {
        private readonly IGuruService _guru;

        public GuruController(IGuruService guru)
        {
            _guru = guru;
        }

        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            // extract user id from token (ClaimTypes.NameIdentifier or 'sub')
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                              ?? User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId)) return BadRequest("Authenticated user id not found in token");
            var dto = await _guru.GetByUserIdAsync(userId);
            return Ok(dto);
        }

        [HttpPost]
        [Authorize(Roles = "Guru,Admin")]
        public async Task<IActionResult> CreateOrUpdate([FromBody] GuruProfileDto dto)
        {
            var res = await _guru.CreateOrUpdateAsync(dto);
            return Ok(res);
        }

        [HttpPut("{id}/verify")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Verify(Guid id)
        {
            await _guru.VerifyAsync(id, true);
            return NoContent();
        }
    }
}
