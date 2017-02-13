using System;
using System.Linq;
using System.Net;
using AutoMapper;
using Geolocation;
using GeoToast.Data;
using GeoToast.Infrastructure.Filters;
using GeoToast.Infrastructure.Services;
using GeoToast.Models;
using MaxMind.GeoIP2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;

namespace GeoToast.Controllers
{
    // Find points in radius
    // https://www.mullie.eu/geographic-searches/

    [Route("api/notifications")]
    [ValidateModel]
    public class PublicNotificationsController : Controller
    {
        private readonly GeoToastDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IIPAddressService _ipAddressService;
        private readonly ILogger _logger;

        public PublicNotificationsController(GeoToastDbContext dbContext, IMapper mapper, IHostingEnvironment hostingEnvironment, IIPAddressService ipAddressService, ILogger<PublicNotificationsController> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _ipAddressService = ipAddressService;
            _logger = logger;
        }

        [HttpGet("{propertyId}")]
        [EnableCors("AllowAllOrigins")]
        public IActionResult Get(int propertyId)
        {
            /*
            using (var reader = new DatabaseReader(_hostingEnvironment.ContentRootPath + "\\GeoLite2-City.mmdb"))
            {
                // Determine the IP Address of the request
                var ipAddress = _ipAddressService.GetRequestIpAddress();
                _logger.LogDebug("IP Address for request: {ipAddress}", ipAddress);

                // Get the city from the IP Address
                var city = reader.City(ipAddress);
                _logger.LogDebug("City for request: {city}", city);

                if (city?.Location?.Latitude == null || city?.Location?.Longitude == null)
                {
                    _logger.LogInformation("Unable to determine city from IP Address {ipAddress}", ipAddress);
                    return BadRequest(); // TODO: No, it is not a bad request. Come back and fix
                }

                // Now that we have the city, determine a 100KM radius around the city coordinates
                Coordinate origin = new Coordinate
                {
                    Latitude = city.Location.Latitude.Value,
                    Longitude = city.Location.Longitude.Value
                };
                CoordinateBoundaries boundaries = new CoordinateBoundaries(origin, 100, DistanceUnit.Kilometers);
                _logger.LogDebug("Boundaries for query: {@Boundaries}", boundaries);

                // Get today's date
                DateTime now = DateTime.Now; // TODO: Need to refactory to make current date testable

                // Now that we have that we can select all relevant notifications
                var notifications = _dbContext.Notifications.Where(
                    x => x.Property.Id == propertyId // Select for the property in questions
                         && (x.StartDate <= now && x.EndDate >= now) // Select active ones
                         && (x.Latitude >= boundaries.MinLatitude && x.Latitude <= boundaries.MaxLatitude) // Select one which should be displayed to users within determined boundaries
                         && (x.Longitude >= boundaries.MinLongitude && x.Longitude <= boundaries.MaxLongitude)
                );

                return Ok(_mapper.Map<List<NotificationReadModel>>(notifications));
            }
            */

            // We need to load the actual notification from the DB, but for now I am just loading one from file
            // to test whether we can inject it successfully into the HTML page
            var resourceStream = this.GetType().GetTypeInfo().Assembly.GetManifestResourceStream("GeoToast.Resources.sample-notification.html");
            using (var streamReader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                return Ok(streamReader.ReadToEnd());
            }
        }

        private double Distance(double lat1, double lng1, double lat2, double lng2, char unit)
        {
            lat1 = Deg2Rad(lat1);
            lng1 = Deg2Rad(lng1);
            lat2 = Deg2Rad(lat2);
            lng2 = Deg2Rad(lng2);

            var distance = Math.Acos(Math.Sin(lat1) * Math.Sin(lat2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Cos(lng1 - lng2));

            // distance in human-readable format:
            // earth's radius in km = ~6371
            return 6371 * distance;

            //double theta = lon1 - lon2;
            //double dist = Math.Sin(Deg2Rad(lat1)) * Math.Sin(Deg2Rad(lat2)) + Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) * Math.Cos(Deg2Rad(theta));
            //dist = Math.Acos(dist);
            //dist = Rad2Deg(dist);
            //dist = dist * 60 * 1.1515;
            //if (unit == 'K')
            //{
            //    dist = dist * 1.609344;
            //}
            //else if (unit == 'N')
            //{
            //    dist = dist * 0.8684;
            //}
            //return (dist);
        }

        private double Deg2Rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private double Rad2Deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}