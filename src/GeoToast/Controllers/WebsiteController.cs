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
    [Route("api/website")]
    public class WebsiteController : Controller
    {
        private readonly GeoToastDbContext _dbContext;
        private readonly IMapper _mapper;

        public WebsiteController(GeoToastDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<WebsiteReadModel> Get()
        {
            // TODO: Only select websites for current User

            return _mapper.Map<List<WebsiteReadModel>>(_dbContext.Websites);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var website = await _dbContext.Websites.FirstOrDefaultAsync(w => w.Id == id);

            if (website == null)
            return NotFound();

            return  Ok(_mapper.Map<WebsiteReadModel>(website));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]WebsiteCreateModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: Set User ID

                var website = _mapper.Map<Website>(model);

                _dbContext.Websites.Add(website);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction("Get", new { id = website.Id }, _mapper.Map<WebsiteReadModel>(website));
            }
            
            return BadRequest();
        }
    }
}