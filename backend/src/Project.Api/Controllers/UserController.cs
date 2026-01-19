using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs;
using Project.Application.Interfaces;

namespace Project.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                              ?? User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId)) return BadRequest("Authenticated user id not found in token");

            var user = await _userRepo.GetByIdWithProfilesAsync(userId);
            if (user == null) return NotFound();

            var dto = _mapper.Map<UserDto>(user);
            return Ok(dto);
        }
    }
}
