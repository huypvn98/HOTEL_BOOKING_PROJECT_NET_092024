using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using VNPAYPaymentBackend;

namespace BookingHotel.Core.Services
{
    public class VnPayService : IVnPayService
    {
        private readonly VnPayConfig _config;

        public VnPayService(IConfiguration configuration)
        {
            _config = configuration.GetSection("Vnpay").Get<VnPayConfig>();
        }

        public string CreatePaymentUrl(PaymentRequest model, HttpContext context)
        {


            var queryParams = new SortedDictionary<string, string>
            {
                { "vnp_Version", _config.Version },
                { "vnp_Command", _config.Command },
                { "vnp_TmnCode", _config.TmnCode },
                { "vnp_Amount", ((int)(model.Amount * 100)).ToString() },
                { "vnp_CurrCode", _config.CurrCode },
                { "vnp_TxnRef", model.TransactionId },
                { "vnp_OrderInfo", model.OrderDescription },
                { "vnp_OrderType", model.OrderType },
                { "vnp_Locale", _config.Locale },
                { "vnp_ReturnUrl", _config.ReturnUrl },
                { "vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss") },
                { "vnp_IpAddr", GetIpAddress(context) }
            };


            var rawData = string.Join("&", queryParams.Select(kvp => $"{kvp.Key}={HttpUtility.UrlEncode(kvp.Value)}"));
            Console.WriteLine($"RawData: {rawData}");


            string hashData = HmacSha512(_config.HashSecret, rawData);
            Console.WriteLine($"Generated SecureHash: {hashData}");

            string paymentUrl = $"{_config.BaseUrl}?{rawData}&vnp_SecureHash={hashData}";
            Console.WriteLine($"Payment URL: {paymentUrl}");

            return paymentUrl;
        }

        private string GetIpAddress(HttpContext context)
        {
            var remoteIpAddress = context.Connection.RemoteIpAddress;
            if (remoteIpAddress != null && remoteIpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
            {

                remoteIpAddress = System.Net.Dns.GetHostEntry(remoteIpAddress)
                                    .AddressList.FirstOrDefault(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            }
            return remoteIpAddress?.ToString() ?? "127.0.0.1";
        }

        private string HmacSha512(string key, string inputData)
        {
            var hash = new StringBuilder();
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }
    }
}
