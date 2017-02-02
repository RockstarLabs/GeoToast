using System.Net;

namespace GeoToast.Infrastructure.Services
{
    public interface IIPAddressService
    {
        IPAddress GetRequestIpAddress();
    }
}