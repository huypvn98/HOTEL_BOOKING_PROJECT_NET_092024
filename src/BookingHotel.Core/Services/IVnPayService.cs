using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BookingHotel.Core.Services
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(PaymentRequest model, HttpContext context);
    }
}