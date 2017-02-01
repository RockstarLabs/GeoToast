using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using GeoToast.Data;
using GeoToast.Data.Models;
using GeoToast.Infrastructure.Filters;
using GeoToast.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeoToast.Controllers
{
    [Authorize]
    [Route("api/properties/{propertyId}/notifications")]
    [ValidateModel]
    public class NotificationsController : Controller
    {
        private readonly GeoToastDbContext _dbContext;
        private readonly IMapper _mapper;

        public NotificationsController(GeoToastDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int propertyId)
        {
            // Get User ID
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            // Get the property
            var property = await _dbContext.Properties.FirstOrDefaultAsync(
                x => x.Id == propertyId && x.UserId == userId);

            // If the property does not exist, return 404
            if (property == null)
                return NotFound();

            // Grab all notifications for this property
            var notifications = _dbContext.Notifications.Where(x => x.Property.Id == propertyId);

            // Return the list of notifications
            return Ok(_mapper.Map<List<NotificationReadModel>>(notifications));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int propertyId, int id)
        {
            // Get User ID
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            // Get notification by Id, Property and User
            var notification = await _dbContext.Notifications.FirstOrDefaultAsync(
                x => x.Id == id && x.Property.Id == propertyId && x.Property.UserId == userId);
            
            if (notification == null)
                return NotFound();

            return Ok(_mapper.Map<NotificationReadModel>(notification));
        }

        [HttpPost]
        public async Task<IActionResult> Post(int propertyId, [FromBody]NotificationCreateModel model)
        {
            // Get User ID
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            // Get the property
            var property = await _dbContext.Properties.FirstOrDefaultAsync(
                x => x.Id == propertyId && x.UserId == userId);

            // If the property does not exist, return 404
            if (property == null)
                return NotFound();

            // Map notification 
            var notification = _mapper.Map<Notification>(model);
            notification.Property = property;

            _dbContext.Notifications.Add(notification);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = notification.Id }, _mapper.Map<NotificationReadModel>(notification));
        }
    }
}