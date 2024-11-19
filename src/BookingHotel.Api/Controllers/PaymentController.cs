using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using BookingHotel.Core.Services;

namespace VNPAYPaymentBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;

        public PaymentController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        [HttpPost("create-payment-url")]
        public IActionResult CreatePaymentUrl([FromBody] PaymentRequest model)
        {
            var paymentUrl = _vnPayService.CreatePaymentUrl(model, HttpContext);
            return Ok(new { Url = paymentUrl });
        }
    }
}
