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
    [Route("api/properties")]
    [ValidateModel]
    public class PropertiesController : Controller
    {
        private readonly GeoToastDbContext _dbContext;
        private readonly IMapper _mapper;

        public PropertiesController(GeoToastDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // Get User ID
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            // Only get properties for this user
            var properties = _dbContext.Properties.Where(x => x.UserId == userId);

            // return mapped list
            return Ok(_mapper.Map<List<PropertyReadModel>>(properties));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            // Get User ID
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            // Get property for this ID and user
            var property = await _dbContext.Properties.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            // 404 if not found
            if (property == null)
                return NotFound();

            // return mapped object
            return Ok(_mapper.Map<PropertyReadModel>(property));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PropertyCreateModel model)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var property = _mapper.Map<Property>(model);
            property.UserId = userId;

            _dbContext.Properties.Add(property);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = property.Id }, _mapper.Map<PropertyReadModel>(property));
        }
    }
}