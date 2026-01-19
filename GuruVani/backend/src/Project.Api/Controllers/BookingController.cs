using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Interfaces;

namespace Project.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _svc;

        public BookingController(IBookingService svc)
        {
            _svc = svc;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookingRequest req)
        {
            var booking = await _svc.CreateAsync(req.CustomerId, req.SlotId);
            return CreatedAtAction(nameof(Get), new { id = booking.Id }, booking);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var b = await _svc.GetByIdAsync(id);
            if (b == null) return NotFound();
            return Ok(b);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> ListByCustomer(Guid customerId)
        {
            var items = await _svc.ListByCustomerAsync(customerId);
            return Ok(items);
        }

        [HttpGet("guru")]
        [Authorize(Roles = "Guru,Admin")]
        public async Task<IActionResult> ListByGuru()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                              ?? User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;
            if (!Guid.TryParse(userIdClaim, out var guruId)) return BadRequest("Authenticated user id not found in token");

            var items = await _svc.ListByGuruAsync(guruId);
            return Ok(items);
        }
    }

    public class CreateBookingRequest
    {
        public Guid CustomerId { get; set; }
        public Guid SlotId { get; set; }
    }
}
