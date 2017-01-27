using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GeoToast.Data;
using GeoToast.Data.Models;
using GeoToast.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeoToast.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]WebsiteCreateModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: Set User ID

                var website = _mapper.Map<Website>(model);

                _dbContext.Websites.Add(website);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction("Get", website.Id);
            }
            
            return BadRequest();
        }
    }
}