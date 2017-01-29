using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GeoToast.Data;
using GeoToast.Data.Models;
using GeoToast.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeoToast.Controllers
{
    //[Authorize]
    [Route("api/properties")]
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
        public IEnumerable<PropertyReadModel> Get()
        {
            // TODO: Only select websites for current User

            return _mapper.Map<List<PropertyReadModel>>(_dbContext.Properties);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var website = await _dbContext.Properties.FirstOrDefaultAsync(w => w.Id == id);

            if (website == null)
            return NotFound();

            return  Ok(_mapper.Map<PropertyReadModel>(website));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PropertyCreateModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: Set User ID

                var website = _mapper.Map<Property>(model);

                _dbContext.Properties.Add(website);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction("Get", new { id = website.Id }, _mapper.Map<PropertyReadModel>(website));
            }
            
            return BadRequest();
        }
    }
}