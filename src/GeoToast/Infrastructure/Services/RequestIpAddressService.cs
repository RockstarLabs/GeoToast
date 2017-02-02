using System;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace GeoToast.Infrastructure.Services
{
    public class RequestIpAddressService : IIPAddressService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestIpAddressService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

        }
        public IPAddress GetRequestIpAddress()
        {
            return _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
        }
    }
}