using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocationServicesApi;
using LocationServicesApi.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace LocationServicesApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/History")]
    public class HistoryController : Controller
    {
        private readonly Context _context;

        public HistoryController(Context context)
        {
            _context = context;
        }

        // GET: api/History/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHistoryForDevice([FromRoute] int id, DateTime from, DateTime to)
        {
            if (to == null)
            {
                to = DateTime.Now;
            }
            var identity = User.Identity as ClaimsIdentity;
            var userId = identity.Claims.Where(c => c.Type == "user_id").FirstOrDefault().Value;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var location = await _context.Locations.Where(x => x.DeviceId == id &&
                x.Device.UserId == userId && x.Date >= from && x.Date < to).ToListAsync();

            if (location == null)
            {
                return NotFound();
            }

            return Ok(location.OrderBy(x => x.Date));
        }
    }
}