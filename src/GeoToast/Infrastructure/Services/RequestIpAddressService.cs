using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace GeoToast.Infrastructure.Services
{
    public class RequestIpAddressService : IIPAddressService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDictionary<string, string> _ipAddressMappings;

        public RequestIpAddressService(IHttpContextAccessor httpContextAccessor,
            IDictionary<string, string> ipAddressMappings)
        {
            _httpContextAccessor = httpContextAccessor;
            _ipAddressMappings = ipAddressMappings;
        }

        public IPAddress GetRequestIpAddress()
        {
            var remoteIpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;

            if (_ipAddressMappings.Any(x => x.Key == remoteIpAddress.ToString()))
            {
                var mappedAddress =
                    _ipAddressMappings.First(x => x.Key == remoteIpAddress.ToString());

                return IPAddress.Parse(mappedAddress.Value);
            }

            return _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
        }
    }
}