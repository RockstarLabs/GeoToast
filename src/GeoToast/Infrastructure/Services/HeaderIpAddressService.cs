using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace GeoToast.Infrastructure.Services
{
    public class HeaderIpAddressService : IIPAddressService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HeaderIpAddressService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

        }
        public IPAddress GetRequestIpAddress()
        {
            string spoofIp = _httpContextAccessor.HttpContext.Request.Headers["X-Spoof-IP-Address"].FirstOrDefault();
            
            if (String.IsNullOrEmpty(spoofIp))
                return IPAddress.Loopback;

            return IPAddress.Parse(spoofIp);
        }
    }
}