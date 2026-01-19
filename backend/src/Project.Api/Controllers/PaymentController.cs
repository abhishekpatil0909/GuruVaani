using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Interfaces;

namespace Project.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _svc;

        public PaymentController(IPaymentService svc)
        {
            _svc = svc;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaymentRequest req)
        {
            var p = await _svc.CreateForBookingAsync(req.BookingId, req.Amount);
            return Ok(p);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var p = await _svc.GetByIdAsync(id);
            if (p == null) return NotFound();
            return Ok(p);
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMyPayments()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
                return Unauthorized();

            var payments = await _svc.GetByCustomerIdAsync(userId);
            return Ok(payments);
        }
    }

    public class CreatePaymentRequest
    {
        public Guid BookingId { get; set; }
        public decimal Amount { get; set; }
    }
}
