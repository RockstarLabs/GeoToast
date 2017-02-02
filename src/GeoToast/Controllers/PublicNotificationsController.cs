using System.Net;
using AutoMapper;
using GeoToast.Data;
using GeoToast.Infrastructure.Filters;
using GeoToast.Infrastructure.Services;
using MaxMind.GeoIP2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace GeoToast.Controllers
{
    [Route("api/notifications")]
    [ValidateModel]
    public class PublicNotificationsController : Controller
    {
        private readonly GeoToastDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IIPAddressService _ipAddressService;

        public PublicNotificationsController(GeoToastDbContext dbContext, IMapper mapper, IHostingEnvironment hostingEnvironment, IIPAddressService ipAddressService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _ipAddressService = ipAddressService;
        }

        [HttpGet("{propertyId}")]
        public IActionResult Get(int propertyId)
        {
            using (var reader = new DatabaseReader(_hostingEnvironment.ContentRootPath + "\\GeoLite2-City.mmdb"))
            {
                var city = reader.City(_ipAddressService.GetRequestIpAddress());

                return Ok(city);
            }
        }
    }
}