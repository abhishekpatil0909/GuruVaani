using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs;
using Project.Application.Interfaces;

namespace Project.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AvailabilityController : ControllerBase
    {
        private readonly IAvailabilityService _svc;

        public AvailabilityController(IAvailabilityService svc)
        {
            _svc = svc;
        }

        [HttpPost]
        [Authorize(Roles = "Guru,Admin")]
        public async Task<IActionResult> Create([FromBody] AvailabilitySlotDto dto)
        {
            // enforce GuruId from authenticated user claims
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                              ?? User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;
            if (!Guid.TryParse(userIdClaim, out var guruId)) return BadRequest("Authenticated user id not found in token");
            dto.GuruId = guruId;
            var res = await _svc.CreateAsync(dto);
            return Ok(res);
        }

        [HttpGet("me")]
        [Authorize(Roles = "Guru,Admin")]
        public async Task<IActionResult> ListByGuru()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                              ?? User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;
            if (!Guid.TryParse(userIdClaim, out var guruId)) return BadRequest("Authenticated user id not found in token");

            var res = await _svc.ListByGuruAsync(guruId);
            return Ok(res);
        }
    }
}
